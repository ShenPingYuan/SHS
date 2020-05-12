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
            CreateMap<Class, ClassDto>().ForMember(d => d.CollegeName, o => o.MapFrom(x => x.College.CollegeName));
            CreateMap<ClassDto, Class>();
        }
    }
}
