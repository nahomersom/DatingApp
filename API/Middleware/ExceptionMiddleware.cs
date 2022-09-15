using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        //requestDelegate it's just pipe like that of observabel
        //ilooger allow us to print
        //IHostEnvironmet it help us to check our env whether prod or normal env
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _looger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> looger, IHostEnvironment env)
        {
            _env = env;
            _looger = looger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context){

            try{
                await _next(context);
            }
            catch(Exception exception){
                _looger.LogError(exception,exception.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode,exception.Message,exception.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode,"Internal Server Error");
                
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response,options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}