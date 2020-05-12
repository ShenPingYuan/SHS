using AutoMapper;
using SHS.Dtos;
using SHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHS.Web.Profiles
{
    public class StudentProfile:Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, NewStudentListDto>()
                .ForMember(d => d.ClassName, o => o.MapFrom(x => x.Class.ClassName));
            CreateMap<Student, StudentDto>()
                .ForMember(d => d.ClassName, o => o.MapFrom(x => x.Class.ClassName));
            CreateMap<Student, StudentInfoDto>()
                .ForMember(d => d.ClassName, o => o.MapFrom(x => x.Class.ClassName));
            CreateMap<StudentInfoDto, Student>();
        }
    }
}
