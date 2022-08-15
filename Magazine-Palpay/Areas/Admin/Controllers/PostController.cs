using Microsoft.AspNetCore.Mvc;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Admin/Post/Create")]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult OnPostCreateOrEdit()
        {
            return View();
        }
    }
}
