using Alumnium.Core;
using Alumnium.Core.DbContext;
using AlumniumWorkshop.Models.AlmniumType;
using AlumniumWorkshop.Models.Item;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlumniumWorkshop.Controllers
{
    public class AluminumTypeController : BaseController
    {
        public AluminumTypeController(RoleManager<IdentityRole> roleMngr, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
          ApplicationDBContext db) : base(roleMngr, configuration, userManager, signInManager, db) { }
        public IActionResult Index()
        {
            var result = CS.GetTypesList();
            if (!result.Result)
                Alert("حدث خطأ", Consts.NotificationType.error);
            return View(result.model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var result = CS.GetModelForCreateType();
            if (!result.Result)
            {
                Alert("حدث خطأ", Consts.NotificationType.error);
                return RedirectToAction(nameof(Index));

            }

            return View(result.model);
        }
        [HttpPost]
        public IActionResult Create([FromBody]CreateAluminumTypeModel model)
        {
            var result = CS.CreateType(model);
            if (!result)
            {
                Alert("حدث خطأ", Consts.NotificationType.error);
                return Json(false);
            }
            return Json(true);
        }

        public IActionResult ChangeStatus(int id)
        {
            var result = CS.ChangeTypeStatus(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = CS.GetModelForEditType(id);
            if (!result.Result)
            {
                Alert("حدث خطأ", Consts.NotificationType.error);
                return RedirectToAction(nameof(Index));

            }

            return View(result.model);
        }
        [HttpPost]
        public IActionResult Edit([FromBody] EditAluminumTypeModel model)
        {
            var result = CS.UpdateType(model);
            if (!result)
            {
                Alert("حدث خطأ", Consts.NotificationType.error);
                return Json(false);
            }
            return Json(true);
        }
    }
}
