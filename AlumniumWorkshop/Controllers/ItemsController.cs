﻿using Microsoft.AspNetCore.Mvc;

namespace AlumniumWorkshop.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}