using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace qrAPI.Infra.Extensions
{
    public static class GeneralExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext) =>
            httpContext.User == null
                ? Guid.Empty
                : Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
    }
}