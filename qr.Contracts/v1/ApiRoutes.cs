﻿namespace qrAPI.Contracts.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Qrs
        {
            private const string Relative = Base + "/qrs";
            public const string GetAll = Relative;
            public const string Get = Relative + "/{qrId}";
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

        public static class MedicalRecords
        {
            private const string Relative = Base + "/pets/{petId}/MedicalRecords";
            public const string GetAll = Relative;
            public const string Get = Relative + "/{medicalRecordId}";
            public const string Update = Relative + "/{medicalRecordId}";
            public const string Delete = Relative + "/{medicalRecordId}";
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