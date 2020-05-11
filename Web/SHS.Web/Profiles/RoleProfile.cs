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
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<ApplicationIdentityRole, RoleDto>()
                .ForMember(d => d.PositionGrade, o => o.MapFrom(x => x.Position))
                .ForMember(d=>d.RoleName,o=>o.MapFrom(x=>x.Name));
        }

    }
}

