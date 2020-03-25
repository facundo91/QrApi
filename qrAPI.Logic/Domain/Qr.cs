using System.Collections.Generic;

namespace qrAPI.Logic.Domain
{
    public class Qr : DomainObject
    {
        public string Name { get; set; }
        public Pet Pet { get; set; }
        public IEnumerable<Scan> Scans { get; set; }
    }
}