using SHS.Data;
using SHS.Entities;
using SHS.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Repository
{
    public class StudentRepository :BaseRepository<Student>,IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
