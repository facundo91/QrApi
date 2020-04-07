using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace qrAPI.Infrastructure.Extensions
{
    public static class GeneralExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext) => 
            httpContext.User == null 
                ? Guid.Empty 
                : Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
    }
}