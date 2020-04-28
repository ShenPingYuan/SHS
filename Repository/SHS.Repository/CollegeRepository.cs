using SHS.Data;
using SHS.Entities;
using SHS.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Repository
{
    public class CollegeRepository:BaseRepository<College>,ICollegeRepository
    {
        public CollegeRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
