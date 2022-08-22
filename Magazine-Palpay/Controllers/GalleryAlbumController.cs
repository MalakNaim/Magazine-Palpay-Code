using Magazine_Palpay.Areas.Admin.Controllers;
using Magazine_Palpay.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Magazine_Palpay.Controllers
{
    public class GalleryAlbumController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public GalleryAlbumController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GalleryAlbum/Index")]
        public IActionResult Index(int id)
        {
            var album = _context.Gallery.Where(x => x.Id.Equals(id)
            && !x.IsDelete).Include(x=>x.GalleryPhoto).FirstOrDefault();
            return View(album);
        }
    }
}
