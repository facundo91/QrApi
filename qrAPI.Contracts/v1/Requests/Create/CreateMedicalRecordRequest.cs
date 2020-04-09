using System;

namespace qrAPI.Contracts.v1.Requests.Create
{
    public class CreateMedicalRecordRequest
    {
        public DateTime Date { get; set; }
        public string Comments { get; set; }
    }
}
