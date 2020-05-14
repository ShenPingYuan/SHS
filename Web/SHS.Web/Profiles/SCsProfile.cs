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
    public class SCsProfile:Profile
    {
        public SCsProfile()
        {
            CreateMap<StudentCourse, StudentCourseDto>().ForMember(d=>d.CourseName,o=>o.MapFrom(x=>x.Course.CourseName))
                .ForMember(d => d.StudentName, o => o.MapFrom(x => x.Student.StudentName));
            CreateMap<StudentCourse, SimpleStudentDto>()
                .ForMember(d => d.StudentName, o => o.MapFrom(x => x.Student.StudentName));
            CreateMap<StudentCourse, EntryScoreDto>().ForMember(d => d.CourseName, o => o.MapFrom(x => x.Course.CourseName))
                .ForMember(d => d.StudentName, o => o.MapFrom(x => x.Student.StudentName));
        }
    }
}
