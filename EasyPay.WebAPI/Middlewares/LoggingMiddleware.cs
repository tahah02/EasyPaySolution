using EasyPay.Data.GeneratedModels; 
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks; 

namespace EasyPay.WebAPI.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, EasyPayDbContext dbContext)
        {
            // 1. Request Read
            context.Request.EnableBuffering();
            var requestBody = await ReadStream(context.Request.Body);
            context.Request.Body.Position = 0;

            string userId = ExtractUserId(requestBody);

            //Header to JSON
            var headerDictionary = new Dictionary<string, string>();

            // Loop to take Headers
            foreach (var key in context.Request.Headers.Keys)
            {
                headerDictionary.Add(key, context.Request.Headers[key]);
            }

            // Extra Info 
            headerDictionary["IP"] = context.Connection.RemoteIpAddress?.ToString();

            // Dictionary to JSON
            string serializedHeaders = JsonSerializer.Serialize(headerDictionary);
            // ------------------------------------------------

            // 2. Response Capture Setup
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

          
            await _next(context);

            string controllerName = context.Request.RouteValues["controller"]?.ToString();
            string actionName = context.Request.RouteValues["action"]?.ToString();
            string path = context.Request.Path.ToString();
            string method = context.Request.Method;

            // 3. Response Read
            context.Response.Body.Position = 0;
            var responseContent = await ReadStream(context.Response.Body);
            context.Response.Body.Position = 0;

            // 4. Save to DB
            var log = new ApiLog
            {
                RequestTime = DateTime.Now,
                UserId = userId,
                RequestBody = requestBody,
                ResponseBody = responseContent,
                StatusCode = context.Response.StatusCode,          
                Controller = controllerName,
                Action = actionName,
                Path = path,
                Method = method,
            };

            dbContext.ApiLogs.Add(log);
            await dbContext.SaveChangesAsync();

            await responseBody.CopyToAsync(originalBodyStream);
        }
        // Helper: Stream to String
        private async Task<string> ReadStream(Stream stream)
        {
            using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
            return await reader.ReadToEndAsync();
        }

        // Helper: JSON parse
        private string ExtractUserId(string body)
        {
            try
            {
                if (string.IsNullOrEmpty(body)) return "Anonymous";
                var json = JsonSerializer.Deserialize<JsonElement>(body);

                if (json.TryGetProperty("fromUser", out var fromUser)) return fromUser.ToString();
                if (json.TryGetProperty("userId", out var uid)) return uid.ToString();

                return "Unknown";
            }
            catch
            {
                return "Error-Parsing";
            }
        }
    }
}