using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHS.Core;
using SHS.Dtos;
using SHS.Entities;

namespace SHS.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public RolesController(RoleManager<ApplicationIdentityRole> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetRoles()
        {
            var roles =await _roleManager.Roles.OrderBy(x=>x.Position).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return new ResultData(ReturnCode.Succeed, dtos.Count(), "角色列表", dtos);
        }
        [HttpDelete("{roleName}")]
        public async Task<ActionResult> DeleteRole(string roleName)
        {
            var role =await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }
            await _roleManager.DeleteAsync(role);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto dto)
        {
            var role =await _roleManager.FindByNameAsync(dto.RoleName);
            if (role != null)
            {
                return BadRequest();
            }
            await _roleManager.CreateAsync(new ApplicationIdentityRole
            {
                Position = (Position)dto.PositionGrade,
                Name = dto.RoleName,
            });
            return NoContent();
        }
    }
}