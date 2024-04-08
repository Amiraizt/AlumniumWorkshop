using Alumnium.Core;
using Alumnium.Core.DbContext;
using AlumniumWorkshop.Areas.Identity.Pages.Account;
using AlumniumWorkshop.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AlumniumWorkshop.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        public UserController(RoleManager<IdentityRole> roleMgr, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDBContext dbContext) : base(roleMgr, configuration, userManager, signInManager, dbContext)
        {

        }


        public async Task<IActionResult> Index()
        {
            var users = _db.Users.ToList().Select(a =>
            {
                var role = _userManager.GetRolesAsync(a).Result;
                return new UserModel
                {
                    Id = a.Id,
                    Email = a.Email,
                    Role = role.FirstOrDefault()
                };

            }).ToList();
            return View(users);
        }

        public IActionResult CreateUser()
        {
            var roles = _roleManager.Roles.ToList();
            List<SelectListItem> rolesList = new List<SelectListItem>();
            foreach (var role in roles)
            {
                rolesList.Add(new SelectListItem { Text = role.Name, Value = role.Name });
            }
            ViewBag.Roles = rolesList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterationModel model)
        {


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,

                };


                var result = await _userManager.CreateAsync((ApplicationUser)user, model.Password);

                if (result.Succeeded)
                {

                    var userId = await _userManager.GetUserIdAsync((ApplicationUser)user);
                    await _userManager.AddToRoleAsync((ApplicationUser)user, model.RoleName);

                    Alert("تمت الاضافة", Consts.NotificationType.success);
                }
                else
                {
                    Alert("خطأ", Consts.NotificationType.error);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Logout()
        {
            var currentUSer = User;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Alert("تمت العملية بنجاح", Consts.NotificationType.success);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult EditUserRole(string id)
        {
            var user = _db.Users.FirstOrDefault(a => a.Id == id);
            var model = new EditUserRole()
            {
                UserId = user.Id,
                NAme = user.UserName
            };
            var roles = _roleManager.Roles.ToList();
            List<SelectListItem> rolesList = new List<SelectListItem>();
            foreach (var role in roles)
            {
                rolesList.Add(new SelectListItem { Text = role.Name, Value = role.Name });
            }
            ViewBag.Roles = rolesList;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserRole(EditUserRole model)
        {
            var result = await CS.EditUserRole(model.UserId, model.Role);
            if (!result)
                Alert("حدث خطأ", Consts.NotificationType.error);
            else
                Alert("تمت العملية بنجاح", Consts.NotificationType.success);
            return RedirectToAction(nameof(Index));
        }
    }
}
