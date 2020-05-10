using AutoMapper;
using SHS.Dtos;
using SHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            CreateMap<Teacher, ListTeacherDto>().ForMember(d => d.RealName, o => o.MapFrom(x => x.TeacherName))
                .ForMember(d => d.Age, o => 
                o.MapFrom(x => DateTime.Now.Year - Convert.ToInt32(x.Birthday.Substring(0, 4))));

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto, Teacher>();
        }
    }
}
