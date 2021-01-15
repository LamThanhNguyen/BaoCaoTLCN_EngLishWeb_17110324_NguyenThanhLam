using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.DTOs;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Helpers;
using WEB_HOCTIENGANH.Interfaces;

namespace WEB_HOCTIENGANH.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // Input username;
        // return người dùng có username = username nhập vào.
        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                    .Where(x => x.UserName == username)
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }

        // Trả về một List User thỏa điều kiện nào đó.
        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();
            // Trả về các username khác với username hiện tại.
            query = query.Where(u => u.UserName != userParams.CurrentUserName);

            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper
                .ConfigurationProvider).AsNoTracking(),
                    userParams.PageNumber, userParams.PageSize);
        }

        // Input id.
        // Return user mà có id trùng với id nhập vào.
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
