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
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public RolesController(RoleManager<ApplicationIdentityRole> roleManager,
            IMapper mapper,
            UserManager<ApplicationIdentityUser> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<ResultData>> GetRoles()
        {
            var roles =await _roleManager.Roles.OrderBy(x=>x.Position).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return new ResultData(ReturnCode.Succeed, dtos.Count(), "角色列表", dtos);
        }
        [HttpGet("simpleroles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetSimpleRoles()
        {
            var roles = await _roleManager.Roles.OrderBy(x => x.Position).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(dtos);
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
        [HttpPost("addtorole")]
        public async Task<ActionResult> AddUserToRole([FromBody]AddUserToRoleDto dto)
        {
            var user =await _userManager.Users.Where(x => x.TeacherId == dto.TeacherId).FirstOrDefaultAsync();
            if(user.UserName=="2439739932")
            {
                return BadRequest();
            }
            var role = await _roleManager.FindByIdAsync(dto.RoleId);
            if (user == null || role == null)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user,roles);
            await _userManager.AddToRoleAsync(user, role.Name);
            return NoContent();
        }
    }
}