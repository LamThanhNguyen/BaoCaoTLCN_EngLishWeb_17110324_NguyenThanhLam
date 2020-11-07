using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Entities;
using System;

namespace WEB_HOCTIENGANH.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }
            //userData = nội dung trong file UserSeedData
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            // JsonSerializer có hai chức năng:
            //      1. Từ một list các giá trị đối tượng hoặc kiểu dữ liệu => Json.
            //      2. Từ một đối tượng Json => List các đối tượng chứa giá trị hoặc các kiểu giá trị.
            // users = Một List các đối tượng kiểu AppUser được chuyển đổi từ đối tượng Json userData.
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null)
            {
                return;
            }
            var roles = new List<AppRole>
            {
                new AppRole { Name = "Member" },
                new AppRole { Name = "Admin" },
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                user.SecurityStamp = Guid.NewGuid().ToString();
                // Tạo ra User với username và password.
                // Tạo ra Quyền Hạn cho các User là Member.
                var resultmember = await userManager.CreateAsync(user, "Password1");
                if (resultmember.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Member");
                }
            }

            //create admin user
            var admin = new AppUser
            {
                UserName = "admin"
            };

            admin.SecurityStamp = Guid.NewGuid().ToString();
            var resultadmin = await userManager.CreateAsync(admin, "Password1");
            if (resultadmin.Succeeded)
            {
                await userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" });
            }
        }
    }
}
