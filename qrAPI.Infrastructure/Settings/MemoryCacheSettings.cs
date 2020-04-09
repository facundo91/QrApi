using System;

namespace qrAPI.Infrastructure.Settings
{
    public class MemoryCacheSettings
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
        public TimeSpan AbsoluteExpirationHours { get; set; }
        public TimeSpan SlidingExpiration { get; set; }
    }
}
