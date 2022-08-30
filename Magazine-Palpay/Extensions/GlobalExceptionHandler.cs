// --------------------------------------------------------------------------------------------------
// <copyright file="GlobalExceptionHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Magazine_Palpay.Application.Wrapper;
using Magazine_Palpay.Web.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace Magazine_Palpay.Web.Extensions
{
    internal class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                 await next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                var responseModel = await ErrorResult<string>.ReturnErrorAsync(exception.Message);
                responseModel.Source = exception.Source;
                responseModel.Exception = exception.Message;
                _logger.LogError(exception.Message);
                switch (exception)
                {
                    case CustomException e:
                        response.StatusCode = responseModel.ErrorCode = (int)e.StatusCode;
                        responseModel.Messages = e.ErrorMessages;
                        break;

                    case KeyNotFoundException:
                        response.StatusCode = responseModel.ErrorCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = responseModel.ErrorCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                string result = string.Empty;
               

                await response.WriteAsync(result);
            }
        }
    }
}