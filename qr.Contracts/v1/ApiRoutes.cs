namespace qrAPI.Contracts.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Qrs
        {
            public const string Relative = Base + "/qrs";
            public const string GetAll = Relative;
            public const string Get = Relative + "/{qrId}";
            public const string Update = Relative + "/{qrId}";
            public const string Delete = Relative + "/{qrId}";
            public const string Create = Relative;
        }

        public static class Pets
        {
            public const string Relative = Base + "/pets";
            public const string GetAll = Relative;
            public const string Get = Relative + "/{petId}";
            public const string Update = Relative + "/{petId}";
            public const string Delete = Relative + "/{petId}";
            public const string Create = Relative;
        }
    }
}