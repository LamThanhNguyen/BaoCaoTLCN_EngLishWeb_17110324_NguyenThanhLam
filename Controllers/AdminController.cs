using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Interfaces;

namespace WEB_HOCTIENGANH.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        // Yêu cầu Policy có các quyền thuộc về Admin
        // return users là một List chứa các User { Id, Username, List<RoleName> }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                    .Include(r => r.UserRoles)
                    .ThenInclude(r => r.Role)
                    .OrderBy(u => u.UserName)
                    .Select(u => new
                    {
                        u.Id,
                        Username = u.UserName,
                        Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                    })
                    .ToListAsync();

            return Ok(users);
        }


        // Đầu vào là username và một chuỗi các roles được ngăn cách nhau bằng dấu phẩy.
        // Return các roles sau khi đã cập nhật của username hiện tại.
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            // Đầu vào là một chuỗi chứa các Roles của username hiện tại được ngăn cách nhau bằng các dấu phẩy.
            // Ta tách các role ra giữa các dấu phẩy và cho nó vào một Array => selectedRoles
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);
            await _userManager.UpdateSecurityStampAsync(user);

            if (user == null)
            {
                return NotFound("Could not find user");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            // Thêm vào user các Roles nằm trong selectedRoles ngoại trừ các role nằm trong userRoles.
            // Tránh được tình trạng thêm trùng vào Role hiện tại user có.
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed to add to roles");
            }

            // Trong các role của user hiện tại. Loại trừ các role của user mà nằm trong userRoles ngoại trừ selectedRoles.
            // Remove các role mà không phải là selectedRoles
            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove from roles");
            }

            return Ok(await _userManager.GetRolesAsync(user));

        }

    }
}
