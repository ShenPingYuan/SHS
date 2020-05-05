using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHS.Dtos;
using SHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHS.Web.MVCControllers.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public readonly UserManager<ApplicationIdentityUser> _userManager;
        public HomeController(UserManager<ApplicationIdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ApplicationIdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            HomeViewModel model = new HomeViewModel
            {
                UserAvatarUrl = user.UserFaceImgUrl,
                NickName=user.NickName
            };
            return View(model);
        }
    }
}
