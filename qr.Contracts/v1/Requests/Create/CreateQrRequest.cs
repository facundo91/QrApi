using System;

namespace qrAPI.Contracts.v1.Requests.Create
{
    public class CreateQrRequest
    {
        public string Name { get; set; }
        public Guid PetId { get; set; }
    }
}