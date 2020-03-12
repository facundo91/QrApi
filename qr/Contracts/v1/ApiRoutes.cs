namespace qr.Contracts.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class Qrs
        {
            public const string GetAll = Base + "/qrs";
            public const string Get = Base + "/qrs/{qrId}";
            public const string Update = Base + "/qrs/{qrId}";
            public const string Delete = Base + "/qrs/{qrId}";
            public const string Create = Base + "/qrs";
        }
    }
}
