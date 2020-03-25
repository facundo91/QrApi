using System;

namespace qrAPI.Logic.Domain
{
    public class MedicalRecord : DomainObject
    {
        public DateTime Date { get; set; }
        public string Comments { get; set; }
    }
}