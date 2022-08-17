using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DepartmentController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public DepartmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Admin/Department/Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Department.ToListAsync());
        }

        [HttpGet("Admin/Department/Create")]
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost("Admin/Department/Create")]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                department.CreatedBy = _userManager.GetUserId(User);
                department.CreatedAt = DateTime.Now;
                department.IsDelete = false;
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        [HttpGet("Admin/Department/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

     
        [HttpPost("Admin/Department/Edit")] 
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    department.UpdatedBy = _userManager.GetUserId(User);
                    department.UpdatedAt = DateTime.Now;
                    department.IsDelete = false;
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

     
        [HttpPost("Admin/Department/Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);
            department.IsDelete = true;
            _context.Department.Update(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
    }
}
