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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Magazine_Palpay.Web.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<string> SaveFile(this IFormFile file, string folder)
        {
            if (file.Length > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileExtension = Path.GetExtension(fileName);
                string newFileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/assets/" + folder + "/", newFileName);

                using (var stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }

                return newFileName;
            }
            return string.Empty;
        }

        public static async Task<string> SaveAsync(this IFormFile file, string folder)
        {
            if(file.Length > 0)
            {
                using (var mStream = new MemoryStream())
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string newFileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/assets/" + folder + "/", newFileName);

                    file.CopyTo(mStream);
                    var fileBytes = mStream.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    file.OpenReadStream();
                    Compressimage(path, "", fileBytes);
                    return newFileName;
                }
            }

           
            return string.Empty;
        }

        public static void Compressimage(string targetPath, String filename, Byte[] byteArrayIn)
        {
            try
            {
                ImageConverter converter = new ImageConverter();
                using (MemoryStream memstr = new MemoryStream(byteArrayIn))
                {
                    using (var image = Image.FromStream(memstr))
                    {
                        float maxHeight = 900.0f;
                        float maxWidth = 900.0f;
                        int newWidth;
                        int newHeight;
                        string extension;
                        Bitmap originalBMP = new Bitmap(memstr);
                        int originalWidth = originalBMP.Width;
                        int originalHeight = originalBMP.Height;

                        if (originalWidth > maxWidth || originalHeight > maxHeight)
                        {

                            // To preserve the aspect ratio  
                            float ratioX = (float)maxWidth / (float)originalWidth;
                            float ratioY = (float)maxHeight / (float)originalHeight;
                            float ratio = Math.Min(ratioX, ratioY);
                            newWidth = (int)(originalWidth * ratio);
                            newHeight = (int)(originalHeight * ratio);
                        }
                        else
                        {
                            newWidth = (int)originalWidth;
                            newHeight = (int)originalHeight;

                        }
                        Bitmap bitMAP1 = new Bitmap(originalBMP, newWidth, newHeight);
                        Graphics imgGraph = Graphics.FromImage(bitMAP1);
                        extension = Path.GetExtension(targetPath);
                        if (extension.ToLower() == ".png" || extension.ToLower() == ".gif" || extension.ToLower() == ".jpeg")
                        {
                            imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                            imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                            bitMAP1.Save(targetPath, image.RawFormat);
                            bitMAP1.Dispose();
                            imgGraph.Dispose();
                            originalBMP.Dispose();
                        }
                        else if (extension.ToLower() == ".jpg")
                        {
                            imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                            imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                            Encoder myEncoder = Encoder.Quality;
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            bitMAP1.Save(targetPath, jpgEncoder, myEncoderParameters);

                            bitMAP1.Dispose();
                            imgGraph.Dispose();
                            originalBMP.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exc = ex.Message;
                throw;

            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}