using SHS.Data;
using SHS.Entities;
using SHS.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Repository
{
    public class StudentCourseRepository :BaseRepository<StudentCourse>,IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
