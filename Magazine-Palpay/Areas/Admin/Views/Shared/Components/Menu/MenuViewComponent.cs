using Microsoft.AspNetCore.Mvc;

namespace Magazine_Palpay.Web.Views.Shared.Components.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}