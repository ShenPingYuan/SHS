using SHS.Data;
using SHS.Entities;
using SHS.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Repository
{
    class CourseRepository:BaseRepository<Course>,ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
