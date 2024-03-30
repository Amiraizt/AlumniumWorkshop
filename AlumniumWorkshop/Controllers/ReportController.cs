using Alumnium.Core.DbContext;
using Alumnium.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.DeliveryCore.Models.Order;

namespace AlumniumWorkshop.Controllers
{
    public class ReportController : BaseController
    {
        public ReportController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
           ApplicationDBContext db) : base(configuration, userManager, signInManager, db) { }
        public IActionResult Index()
        {
            CS.PrepareItemsConsumingReportModel(DateTime.Now.AddDays(-7), DateTime.Now);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ViewItemsReport(DateTime startDate,DateTime endDate)
        {
            var result = CS.PrepareItemsConsumingReportModel(startDate, endDate);
            if (!result.result)
                return RedirectToAction("Index", "Items");
           GenerateItemsReport report = new GenerateItemsReport();
            byte[] abytesReport = report.PrepareReport(result.report);

            return File(abytesReport, "application/pdf");

        }
    }
}
