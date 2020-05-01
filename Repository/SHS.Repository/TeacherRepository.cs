using SHS.Data;
using SHS.Entities;
using SHS.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Repository
{
    public class TeacherRepository:BaseRepository<Teacher>,ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    }
}
