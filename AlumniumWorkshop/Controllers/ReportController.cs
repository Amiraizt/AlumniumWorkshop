﻿using Alumnium.Core.DbContext;
using Alumnium.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.DeliveryCore.Models.Order;
using AlumniumWorkshop.Models.Reports;
using Microsoft.AspNetCore.Authorization;

namespace AlumniumWorkshop.Controllers
{
    [Authorize]
    public class ReportController : BaseController
    {
        public ReportController(RoleManager<IdentityRole> roleMngr, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
           ApplicationDBContext db) : base(roleMngr, configuration, userManager, signInManager, db) { }
        public IActionResult Index()
        {
            CS.PrepareItemsConsumingReportModel(DateTime.Now.AddDays(-7), DateTime.Now);
            return View();
        }

        public IActionResult ItemsReport()
        {
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

        public IActionResult SiteGeneralReport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ViewSitesGeneralReport(DateTime startDate, DateTime endDate)
        {
            var result = CS.PrepareSitesGeneralReportModel(startDate, endDate);
            if (!result.result)
                return RedirectToAction("Index", "SiteRequest");
            GenerateSitesGeneralReport report = new GenerateSitesGeneralReport();
            byte[] abytesReport = report.PrepareReport(result.report);

            return File(abytesReport, "application/pdf");

        }
        public async Task<IActionResult> ViewSiteReport(int id)
        {
            var result = CS.PrepareSiteReportModel(id);
            if (!result.result)
                return RedirectToAction("Index", "SiteRequest");
            GenerateSiteReport report = new GenerateSiteReport();
            byte[] abytesReport = report.PrepareReport(result.report);

            return File(abytesReport, "application/pdf");

        }

        public async Task<IActionResult> ViewStoreReport()
        {
            var result = CS.PrepareStoreReportModel();
            if (!result.result)
                return RedirectToAction("Index", "Items");
            GenerateStoreReport report = new GenerateStoreReport();
            byte[] abytesReport = report.PrepareReport(result.report);

            return File(abytesReport, "application/pdf");

        }

        public IActionResult ItemsLogReport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ViewItemsLogReport(DateTime startDate, DateTime endDate)
        {
            var result = CS.PrepareItemsStoreReportModel(startDate, endDate);
            if (!result.result)
                return RedirectToAction("Index", "Items");
            GenerateItemsLogReport report = new GenerateItemsLogReport();
            byte[] abytesReport = report.PrepareReport(result.report);

            return File(abytesReport, "application/pdf");

        }
    }
}
