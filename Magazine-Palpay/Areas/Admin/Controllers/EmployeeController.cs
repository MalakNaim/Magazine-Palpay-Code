using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Http;
using Magazine_Palpay.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EmployeeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

       [HttpGet("Admin/Employee/Index")]
        public async Task<IActionResult> Index()
        {
            var department = _context.Department.Where(x => !x.IsDelete).ToList(); 
            ViewBag.Department = new SelectList(department, "Id", "Name"); 
            return View();
        }
        [HttpPost("Admin/Employee/LoadAll")]
        public async Task<JsonResult> LoadEmployeeAsync(IFormCollection form)
        {
            string draw = Request.Form["draw"].FirstOrDefault();
            string start = Request.Form["start"].FirstOrDefault();
            string length = Request.Form["length"].FirstOrDefault();
            string sortColumn = Request.Form["columns[" + Request.Form["form[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortColumnDirection = Request.Form["form[0][dir]"].FirstOrDefault();
            string name = form["Name"].ToString();
            int.TryParse(form["Department"], out int department);
            int page_start = int.Parse(start);
            int page_length = int.Parse(length);
            page_start = page_start / page_length;
            page_start = page_start + 1;
            var queryable = _context.Employee.Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                queryable = queryable.Where(x => x.Name.Contains(name));
            } 
          
            if (department > 0)
            {
                queryable = queryable.Where(x => x.DepartmentId.Equals(department));
            }

            var productList = await queryable.ToPaginatedListAsync(page_start, page_length);
            var data = productList.Data.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                JobTitle = x.JobTitle,
                Department = x.DepartmentId != null || x.DepartmentId == 0? _context.Department.Find(x.DepartmentId).Name : "-",
            }).ToList();

            var jsonData = new { data = data, recordsFiltered = productList.TotalCount, recordsTotal = productList.TotalCount };
            return new JsonResult(jsonData);

        }


        [HttpGet("Admin/Employee/Create")]
        public IActionResult Create()
        {
            var department = _context.Department.Where(x => !x.IsDelete).ToList();
            ViewBag.Department = new SelectList(department, "Id", "Name");
            return View();
        }
      
        [HttpPost("Admin/Employee/Create")]
        public async Task<IActionResult> Create(Employee employee, IFormFile empPhoto)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (empPhoto != null && empPhoto.Length > 0)
                {
                    var file = await FormFileExtensions.SaveAsync(empPhoto, "UploadImages");
                    fileName = file;
                }
                employee.Photo = fileName;
                employee.CreatedBy = _userManager.GetUserId(User);
                employee.CreatedAt = DateTime.Now;
                employee.IsDelete = false;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpGet("Admin/Employee/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            var department = _context.Department.Where(x => !x.IsDelete).ToList();
            ViewBag.Department = new SelectList(department, "Id", "Name", employee.Department);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

       
        [HttpPost("Admin/Employee/Edit")]
        public async Task<IActionResult> Edit(int id, Employee employee, IFormFile empPhoto)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = "";
                    if (empPhoto != null && empPhoto.Length > 0)
                    {
                        var file = await FormFileExtensions.SaveAsync(empPhoto, "UploadImages");
                        fileName = file;
                    }
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        employee.Photo = fileName; 
                    }
                    employee.UpdatedBy = _userManager.GetUserId(User);
                    employee.UpdatedAt = DateTime.Now;
                    employee.IsDelete = false;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpPost("Admin/Employee/Delete")]
        public async Task<JsonResult> OnPostDelete(int? id)
        {
            var employee = await _context.Employee.FindAsync(id);
            employee.IsDelete = true;
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();
            Notify.Success("تمت عملية الحذف بنجاح");
            return new JsonResult(new
            {
                isValid = true,
                actionType = "redirect",
                redirectUrl = "/Admin/Employee/Index"
            });
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
