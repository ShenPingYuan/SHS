﻿using SHS.Data;
using SHS.Entities;
using SHS.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Repository
{
    public class ClassRepository:BaseRepository<Class>,IClassRepository
    {
        public ClassRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
