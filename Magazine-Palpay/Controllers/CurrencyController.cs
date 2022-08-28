using Magazine_Palpay.Data.Models;
using Magazine_Palpay.Web.Services;
using Magazine_Palpay.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magazine_Palpay.Web.Controllers
{
    public class CurrencyController : Controller
    {
        private IExchangeRateService exchangeService;
        private IEnumerable<SelectListItem> currencies;

        public CurrencyController(IExchangeRateService exchangeService)
        {
            this.exchangeService = exchangeService;
            currencies = GetCurrenciesAsync().Result ?? new List<SelectListItem>();
        }
        public IActionResult Index()
        {
            return View(new CurrencyViewModel()
            {
                Amount = 1,
                Rate = exchangeService.GetCurrencyRates(1, "USD", "RUB").Result,
                FromSelectedCode = "USD",
                ToSelectedCode = "RUB",
                Currencies = currencies
            });
        }

        [HttpPost]
        public IActionResult Swap(CurrencyViewModel model)
        {
            var newModel = new CurrencyViewModel()
            {
                Amount = model.Amount,
                Rate = 1 / model.Rate,
                FromSelectedCode = model.ToSelectedCode,
                ToSelectedCode = model.FromSelectedCode,
                Currencies = currencies
            };

            ModelState.Clear();

            return View("Index", newModel);
        }

        [HttpPost]
        public IActionResult OnPostModel(CurrencyViewModel model)
        {
            var newModel = new CurrencyViewModel()
            {
                Amount = model.Amount,
                Rate = exchangeService.GetCurrencyRates(model.Amount, model.FromSelectedCode, model.ToSelectedCode).Result,
                FromSelectedCode = model.FromSelectedCode,
                ToSelectedCode = model.ToSelectedCode,
                Currencies = currencies
            };

            ModelState.Clear();

            return View("Index", newModel);
        }


        private async Task<IEnumerable<SelectListItem>> GetCurrenciesAsync()
        {

            var currencies = new List<SelectListItem>();

            foreach (Currency currency in await exchangeService.GetCurrenciesNames())
            {
                currencies.Add(new SelectListItem
                {
                    Value = currency.Code,
                    Text = $"{currency.Code} - {currency.Name}"
                });
            }

            return new SelectList(currencies, "Value", "Text");
        }
    }
}
