using Magazine_Palpay.Data;
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
            var employees = _context.Employee.Where(x => !x.IsDelete)
                .Include(x=>x.Department).ToList();
            int countRow = _context.Employee.Where(x => !x.IsDelete).Count();
            double perPage = 8;
            double NumberOfPages = Math.Ceiling(countRow / perPage);
            if (page < 1 || page > NumberOfPages)
            {
                page = 1;
            }
            int skipValue = (page - 1) * (int)perPage;
            ViewBag.NumberOfPages = NumberOfPages;
            var employeeLst = _context.Employee
                .Where(x=>!x.IsDelete)
                .Include(x => x.Department).OrderByDescending(x => x.CreatedAt)
                .Skip(skipValue).Take((int)perPage).ToList();
            ViewBag.page = page;
            return View(employeeLst);
        }
    }
}
