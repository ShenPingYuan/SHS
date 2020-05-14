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
    public class ClassProfile:Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassDto>().ForMember(d => d.CollegeName, o => o.MapFrom(x => x.College.CollegeName))
                .ForMember(d=>d.TeacherId,o=>o.MapFrom(x=>x.InstructorId));
            CreateMap<ClassDto, Class>().ForMember(d => d.InstructorId, o => o.MapFrom(x => x.TeacherId))
                .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null));
        }
    }
}
