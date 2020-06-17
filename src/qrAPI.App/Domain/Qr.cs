namespace qrAPI.App.Domain
{
    public class Qr : DomainObject
    {
        public string Name { get; set; }
        public Pet Pet { get; set; }
    }
}