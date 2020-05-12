using AutoMapper;
using SHS.Dtos;
using SHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHS.Web.Profiles
{
    public class CollegeProfile:Profile
    {
        public CollegeProfile()
        {
            CreateMap<College, CollegeDto>().ForMember(d => d.TeacherId, o => o.MapFrom(x => x.DeanId));
            CreateMap<CollegeDto, College>().ForMember(d => d.DeanId, o => o.MapFrom(x => x.TeacherId)); ;
        }
    }
}
