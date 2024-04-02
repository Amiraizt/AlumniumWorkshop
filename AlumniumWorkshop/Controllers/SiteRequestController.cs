using Alumnium.Core.DbContext;
using Alumnium.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AlumniumWorkshop.Models.SiteRequest;
using AlumniumWorkshop.Models.AlmniumType;
using Microsoft.EntityFrameworkCore;

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
            List<JsonAluminumDetailsModel> list = new List<JsonAluminumDetailsModel>();
            foreach(var alm in model.Aluminums)
            {
                var items = _db.AluminumUsedItems.Where(a => a.AluminumTypeId == alm.AluminumTypeId).Include(a=>a.Item).ToList();
                var jsonModel = new JsonAluminumDetailsModel
                {
                    AluminumName = _db.AlumniumTypes.FirstOrDefault(a => a.Id == alm.AluminumTypeId).TypeName,
                    Items = items.Select(a => new JsonAluminumDetailsModel.ItemModel
                    {
                        ItemName = a.Item.Name,
                        Qty = (int)(a.ItemQuantity * model.MetersNumber),
                        UnitPrice = (double)a.Item.Price,
                        TotalPrice = (double)(a.Item.Price * model.MetersNumber * a.ItemQuantity)
                    }).ToList()
                };
                list.Add(jsonModel);
            }
            var respnseModel = new SiteJsonResponseModel() {
                aluminums = list,
                TotalPrice = (double)result
              
            };
            return Json(respnseModel);
        }
        public IActionResult Delete(int id)
        {
            var result = CS.DeleteSiteRequest(id);
            if (!result)
                Alert("حدث خطأ", Consts.NotificationType.error);
            else
                Alert("تم التعديل", Consts.NotificationType.success);
            return RedirectToAction(nameof(Index));
        }
    }
}
