// --------------------------------------------------------------------------------------------------
// <copyright file="FormFileExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace Magazine_Palpay.Web.Extensions
{
    public static class FormFileExtensions
    {
        public static async System.Threading.Tasks.Task<string> SaveAsync(this IFormFile file, string folder)
        {
            if(file.Length > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileExtension = Path.GetExtension(fileName);
                string newFileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/assets/"+folder+"/", newFileName);

                using (var stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }

                return newFileName;
            }

            return string.Empty;
        }
    }
}