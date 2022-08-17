using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Localization;

namespace Magazine_Palpay.Web.Exceptions
{
    public class CustomValidationException : CustomException
    {
        public CustomValidationException(IStringLocalizer localizer, List<string> errors)
            : base(localizer["One or more validation failures have occurred."], errors, HttpStatusCode.BadRequest)
        {
        }
    }
}