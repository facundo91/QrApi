using System;

namespace qrAPI.App.Domain
{
    public class Scan
    {
        public Qr Qr { get; set; }
        public DateTime DateTime { get; set; }
        public Location Location { get; set; }
        public bool Notified { get; set; }
        public bool Read { get; set; }
        public Uri PictureUrl { get; set; }
        public string Message { get; set; }
    }
}