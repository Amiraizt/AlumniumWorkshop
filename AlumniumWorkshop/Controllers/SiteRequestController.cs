using Microsoft.AspNetCore.Mvc;

namespace AlumniumWorkshop.Controllers
{
    public class SiteRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
