using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Magazine_Palpay.Web.ViewModels
{
    public class CurrencyViewModel
    {
        public decimal Amount { get; set; }
        public double Rate { get; set; }

        public string OneUnitFromRate => Format(Decimal.Round(Amount * (decimal)Rate, 4));
        public string OneUnitToRate => Format(Decimal.Round(Amount / (decimal)Rate, 4));
        public string FromSelectedCode { get; set; }
        public string ToSelectedCode { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; } = new List<SelectListItem>();

        public string Format(decimal value)
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            return value.ToString("#,0.###", nfi);
        }

    }
}
