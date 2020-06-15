namespace qrAPI.Contracts.v1
{
    public static class ApiRoutes
    {
        private const string Base = "api";

        public static class Qrs
        {
            private const string Relative = Base + "/qrs";
            public const string GetAll = Relative;
            public const string Get = Relative + "/{qrId}";
            public const string Scan = Relative + "/{qrId}/Scan";
            public const string Update = Relative + "/{qrId}";
            public const string Delete = Relative + "/{qrId}";
            public const string Create = Relative;
        }

        public static class Pets
        {
            private const string Relative = Base + "/pets";
            public const string GetAll = Relative;
            public const string Get = Relative + "/{petId}";
            public const string Update = Relative + "/{petId}";
            public const string Delete = Relative + "/{petId}";
            public const string Create = Relative;
        }

        public static class Identity
        {
            private const string Relative = Base + "/identity";
            public const string Login = Relative + "/login";
            public const string Register = Relative + "/register";
            public const string Refresh = Relative + "/refresh";
        }
    }
}