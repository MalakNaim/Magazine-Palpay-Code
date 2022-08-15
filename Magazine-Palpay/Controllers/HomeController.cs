using Microsoft.AspNetCore.Mvc;

namespace Magazine_Palpay.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
