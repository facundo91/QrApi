using System.Linq;
using Microsoft.AspNetCore.Http;

namespace qrAPI.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext) => 
            httpContext.User == null ? 
                string.Empty : 
                httpContext.User.Claims.Single(x => x.Type == "id").Value;
    }
}