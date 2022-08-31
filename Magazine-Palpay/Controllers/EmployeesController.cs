﻿using Magazine_Palpay.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Magazine_Palpay.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Employees/Index")]
        public IActionResult Index(int page = 0)
        {
            int countRow = _context.Employee
                .Where(x => !x.IsDelete).Count();
            double perPage = 12;
            double NumberOfPages = Math.Ceiling(countRow / perPage);
            if (page < 1 || page > NumberOfPages)
            {
                page = 1;
            }
            int skipValue = (page - 1) * (int)perPage;
            ViewBag.NumberOfPages = NumberOfPages;
            var employeeLst = _context.Employee
                .Include(x => x.Department)
                .Where(x=>!x.IsDelete)
                .OrderBy(x => x.Order)
                .Skip(skipValue).Take((int)perPage).ToList();
            ViewBag.page = page;
            return View(employeeLst);
        }
    }
}
