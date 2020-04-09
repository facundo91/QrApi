using System.ComponentModel.DataAnnotations;

namespace qrAPI.Contracts.v1.Requests
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}