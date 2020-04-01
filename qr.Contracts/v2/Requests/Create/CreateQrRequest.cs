using System;

namespace qrAPI.Contracts.v2.Requests.Create
{
    public class CreateQrRequest
    {
        public string Name { get; set; }
        public Guid PetId { get; set; }
    }
}