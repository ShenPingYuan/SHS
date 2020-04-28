using SHS.Data;
using SHS.Entities;
using SHS.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Repository
{
    class StudentRepository:BaseRepository<Student>,IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
