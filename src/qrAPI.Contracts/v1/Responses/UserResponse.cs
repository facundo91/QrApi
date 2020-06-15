using Microsoft.AspNetCore.Identity;
using System;

namespace qrAPI.Contracts.v1.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public IdentityUser Identity { get; set; }
    }
}
