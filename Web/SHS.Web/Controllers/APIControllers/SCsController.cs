using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHS.IRepository;

namespace SHS.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SCsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentCourseRepository _studentCourseRepository;
        public SCsController(IMapper mapper,IStudentCourseRepository studentCourseRepository)
        {
            _mapper = mapper;
            _studentCourseRepository = studentCourseRepository;
        }

    }
}