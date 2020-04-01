using System;

namespace qrAPI.Contracts.v2.Requests.Create
{
    public class CreateMedicalRecordRequest
    {
        public DateTime Date { get; set; }
        public string Comments { get; set; }
    }
}
