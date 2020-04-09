using System;

namespace qrAPI.Contracts.v1.Responses
{
    public class MedicalRecordResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
    }
}
