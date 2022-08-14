using Magazine_Palpay.Application.Features.MagazineLookup.Commands;
using Magazine_Palpay.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    public class MagazineLookupController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrUpdateAsync(MagazineLookupVM model)
        {
            if (ModelState.IsValid)
            {
                var createLookupCommand = Mapper.Map<RegistrationMagazineLookupCommand>(model);
                var result = await Mediator.Send(createLookupCommand);
                if (result.Succeeded)
                {
                    return View();
                   // Notify.Information($"تمت عملية الإضافة بنجاح");
                   // return new JsonResult(new { isValid = true, redirectUrl = "/Tasks/EvaluationQuestions/Search" });
                }
            }
            return View();
        }
    }
}
