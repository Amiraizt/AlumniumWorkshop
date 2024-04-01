using Alumnium.Core.DbContext;
using Alumnium.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AlumniumWorkshop.Models.SiteRequest;

namespace AlumniumWorkshop.Controllers
{
    public class SiteRequestController : BaseController
    {
        public SiteRequestController(RoleManager<IdentityRole> roleMngr,IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
         ApplicationDBContext db) : base(roleMngr, configuration, userManager, signInManager, db) { }
        public IActionResult Index()
        {
            var result = CS.GetSiteRequestsList();
            if(!result.Result)
                Alert("حدث خطأ ما !", Consts.NotificationType.error);
            return View(result.model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var result = CS.GetModelForSiteRequest();
            if (!result.Result)
                Alert("حدث خطأ ما !", Consts.NotificationType.error);
            return View(result.model);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSiteRequestModel model)
        {
            var result = CS.CreateSiteRequest(model);
            if (!result.Result)
                return Json(result.response);
            else
                return Json(true);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = CS.GetEditSiteRequestModel(id);
            if (!result.Result)
                Alert("حدث خطأ ما !", Consts.NotificationType.error);
            return View(result.model);
        }

        [HttpPost]
        public IActionResult Edit(EditSiteRequestModel model)
        {
            var result = CS.UpdateSiteRequest(model);
            if (!result)
                Alert("حدث خطأ ما !", Consts.NotificationType.error);
            else
                Alert("تم التعديل", Consts.NotificationType.success);

            return RedirectToAction("Edit",model.Id);
        }

        public IActionResult CalculateSiteTotal([FromBody]CreateSiteRequestModel model)
        {
            var result = CS.CalculateSiteRequestTotal(model.Aluminums.ToList(), model.MetersNumber);
            return Json(result);
        }
    }
}
