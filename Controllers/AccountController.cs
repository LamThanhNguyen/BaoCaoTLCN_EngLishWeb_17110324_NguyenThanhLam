using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.DTOs;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Interfaces;

namespace WEB_HOCTIENGANH.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        // registerDto = UserName, KnownAs, Gender, DateOfBirth, City, Country, Password
        [HttpPost("register")]
        public async Task<ActionResult<DoiTuongTraVe>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
            {
                return new DoiTuongTraVe
                {
                    Name = "Đăng Ký Tồn Tại!",
                    State = "Thất Bại!"
                };
            }

            var user = _mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Username.ToLower();
            user.SecurityStamp = Guid.NewGuid().ToString();
            // Lưu một User vào Database.
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            // Quyền hạn của user vừa đăng ký là Member
            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return new DoiTuongTraVe
            {
                Name = "Đăng Ký Thành Công!",
                State = "Thành Công!"
            };
        }

        // loginDto = Username, Password
        // return Json = Username, Token, PhotoUrl, KnownAs, Gender 
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.PhotoUrl,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
