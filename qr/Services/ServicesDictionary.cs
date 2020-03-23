using System;
using System.Collections.Generic;
using qrAPI.Domain;

namespace qrAPI.Services
{
    public static class ServicesDictionary
    {
        public static readonly IDictionary<Type, int> TypeDictionary = new Dictionary<Type, int>{
            {typeof(Qr),0},
            {typeof(Pet),1}
        };
    }
}