using AutoMapper;
using SHS.Dtos;
using SHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHS.Web.Profiles
{
    public class TeacherProfile:Profile
    {
        public TeacherProfile()
        {
            CreateMap<UserInfoUpdateDto, Teacher>()
            .ForMember(d => d.Sex, o => o.MapFrom(x => x.UserSex))
                .ForMember(d => d.UserDescription, o => o.MapFrom(x => x.UserDesc))
                .ForMember(d => d.TeacherName, o => o.MapFrom(x => x.RealName))
                .ForMember(d => d.Birthday, o => o.MapFrom(x => x.BirthDate));
        }
    }
}
