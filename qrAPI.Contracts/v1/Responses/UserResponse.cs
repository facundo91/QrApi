using System;
using Microsoft.AspNetCore.Identity;

namespace qrAPI.Contracts.v1.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public IdentityUser Identity { get; set; }
    }
}
