using System;

namespace qrAPI.Contracts.v1.Responses
{
    public class QrResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PetId { get; set; }
    }
}