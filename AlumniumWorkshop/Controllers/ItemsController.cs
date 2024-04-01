using Alumnium.Core.DbContext;
using Alumnium.Core;
using AlumniumWorkshop.Models.Item;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlumniumWorkshop.Controllers
{
    public class ItemsController : BaseController
    {

        public ItemsController(RoleManager<IdentityRole> roleMngr, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ApplicationDBContext db) : base(roleMngr,configuration, userManager,signInManager,db) { }
        public IActionResult Index()
        {
            var items = CS.GetItemsList();
            if(!items.Result)
                Alert("حدث خطأ", Consts.NotificationType.error);

            return View(items.model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ItemModel model)
        {
            var result = CS.CreateItem(model);
            if (!result)
            {
                Alert("حدث خطأ", Consts.NotificationType.error);
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = CS.GetItemById(id);
            if (!result.Result)
            {
                Alert("حدث خطأ", Consts.NotificationType.error);
                return RedirectToAction(nameof(Index));
            }
            return View(result.item);
        }
        [HttpPost]
        public IActionResult Edit(ItemModel model)
        {
            var result = CS.UpdateItem(model);
            if (!result)
            {
                Alert("حدث خطأ", Consts.NotificationType.error);
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ChangeStatus(int id)
        {
            var result = CS.ChangeItemStatus(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
