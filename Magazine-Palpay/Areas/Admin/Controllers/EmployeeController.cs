using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View(await _context.Employee.ToListAsync());
        }

        [HttpGet("Admin/Employee/Create")]
        public IActionResult Create()
        {
            return View();
        }
      
        [HttpPost("Admin/Employee/Create")]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
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
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

       
        [HttpPost("Admin/Employee/Edit")]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            employee.IsDelete = true;
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
