using System.Collections.Generic;

namespace qrAPI.Contracts.v2.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}