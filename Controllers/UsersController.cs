using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.DTOs;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Extensions;
using WEB_HOCTIENGANH.Helpers;
using WEB_HOCTIENGANH.Interfaces;

namespace WEB_HOCTIENGANH.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            userParams.CurrentUserName = User.GetUsername();

            var users = await _unitOfWork.UserRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetMemberAsync(username);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            await _userManager.UpdateSecurityStampAsync(user);

            _mapper.Map(memberUpdateDto, user);

            _unitOfWork.UserRepository.Update(user);

            if (await _unitOfWork.Complete())
            {
                return NoContent();
            }

            return BadRequest("Failed to update user");
        }
    }
}
