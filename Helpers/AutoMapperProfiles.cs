using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Dtos;
using WEB_HOCTIENGANH.DTOs;
using WEB_HOCTIENGANH.Entities;

namespace WEB_HOCTIENGANH.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, MemberDto>();
        }
    }
}
