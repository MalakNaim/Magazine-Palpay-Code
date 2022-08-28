using Magazine_Palpay.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magazine_Palpay.Web.Services
{
    public interface IExchangeRateService
    {
        Task<List<Currency>> GetCurrenciesNames();
        Task<double> GetCurrencyRates(decimal amount, string from, string to);
    }
}
