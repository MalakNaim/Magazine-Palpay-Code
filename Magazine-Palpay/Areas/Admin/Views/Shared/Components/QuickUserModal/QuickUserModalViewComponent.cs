// --------------------------------------------------------------------------------------------------
// <copyright file="QuickUserModalViewComponent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;

namespace Magazine_Palpay.Web.Views.Shared.Components.QuickUserModal
{
    public class QuickUserModalViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}