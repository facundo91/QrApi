using Microsoft.AspNetCore.Mvc;

namespace qrAPI.Contracts
{
    public static class ApiVersions
    {
        public static readonly ApiVersion V1 = new ApiVersion(1, 0);
        public const string V1Tag = "1.0";
    }
}