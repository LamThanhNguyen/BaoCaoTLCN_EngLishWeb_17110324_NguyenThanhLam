using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Dtos;
using WEB_HOCTIENGANH.Models;

namespace WEB_HOCTIENGANH.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            // Cấu hình để chuyển đổi từ UserForRegisterDto => User
            CreateMap<UserForRegisterDto, User>();
        }
    }
}
