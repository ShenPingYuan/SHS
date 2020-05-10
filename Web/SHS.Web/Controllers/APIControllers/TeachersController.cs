using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHS.Core;
using SHS.Entities;
using SHS.IRepository;

namespace SHS.Web.Controllers.APIControllers
{
    [Route("api/[controller]/{teacherid}")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly ITeacherRepository _teacherRepository;
        public TeachersController(UserManager<ApplicationIdentityUser> userManager,
            ITeacherRepository teacherRepository)
        {
            _userManager = userManager;
            _teacherRepository = teacherRepository;
        }
        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetTeachers()
        {
            var teachers =await _teacherRepository.GetAllEntitiesAsIQueryable().ToListAsync();

            return Ok();
        }

        // GET: api/Teacher/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Teacher
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
