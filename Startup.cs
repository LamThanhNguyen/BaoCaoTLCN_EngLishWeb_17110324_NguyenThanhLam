using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WEB_HOCTIENGANH.Data;
using WEB_HOCTIENGANH.Helpers;
using WEB_HOCTIENGANH.Models;

namespace WEB_HOCTIENGANH
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Cấu hình password cho User
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false; // Yêu cầu chữ số
                opt.Password.RequiredLength = 4;    // Yêu cầu độ dài
                opt.Password.RequireNonAlphanumeric = false;    // Ký tự đặc biệt : No
                opt.Password.RequireUppercase = false;  // Yêu cầu chữ hoa : No
            });

            // Đăng ký Identity : 
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);

            // Đăng ký Database Context
            builder.AddEntityFrameworkStores<DataContext>();

            builder.AddRoleValidator<RoleValidator<Role>>();

            builder.AddRoleManager<RoleManager<Role>>();

            builder.AddSignInManager<SignInManager<User>>();


            // Cấu hình Token JwtBearer cho việc xác định có đúng user.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Cấu hình Thêm một số Policy tạo ra các quyền hạn
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
                options.AddPolicy("VipOnly", policy => policy.RequireRole("VIP"));
            });



            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            // Đăng ký Cors.
            services.AddCors();

            // Cấu hình AutoMapper
            services.AddAutoMapper(typeof(DatingRepository).Assembly);

            // Đăng ký khởi chạy Seed.cs
            services.AddTransient<Seed>();
            // Đăng ký Scoped
            services.AddScoped<IDatingRepository, DatingRepository>();
            services.AddScoped<LogUserActivity>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Cấu hình Cors cho API
            app.UseCors(x => x.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            // Xác thực là ai
            app.UseAuthentication();

            // Xác thực có những quyền nào
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
