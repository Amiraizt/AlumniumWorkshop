using Alumnium.Core.DbContext;
using Alumnium.Core;
using AlumniumWorkshop.Models.Item;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace AlumniumWorkshop.Controllers
{
    [Authorize]
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
        [HttpGet]
        public IActionResult AddNewItemQuantity()
        {
            var items = _db.Items.Where(a=>a.Status != Consts.DELETED).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in items)
            {
                list.Add(new SelectListItem { Text = item.Name , Value = item.Id.ToString()});
            }
            ViewBag.Items = list;
            return View();
        }
        [HttpPost]
        public IActionResult AddNewItemQuantity(NewItemQuantityModel model)
        {
            var result = CS.EditItemWarehouse(model);
            if (!result)
            {
                Alert("حدث خطأ", Consts.NotificationType.error);
            }
            else
            {
                Alert("تم التعديل", Consts.NotificationType.success);
            }
            return RedirectToAction(nameof(AddNewItemQuantity));
        }

        public IActionResult Delete(int id)
        {
            var result = CS.DeleteItem(id);
            if (!result)
                Alert("حدث خطأ", Consts.NotificationType.error);
            else
                Alert("تم التعديل", Consts.NotificationType.success);
            return RedirectToAction(nameof(Index));
        }
    }
}
