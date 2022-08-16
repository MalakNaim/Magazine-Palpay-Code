﻿using Microsoft.AspNetCore.Mvc;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [HttpGet("Admin/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}