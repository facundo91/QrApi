using System;
using System.Collections.Generic;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public static class ServicesDictionary
    {
        public static readonly IDictionary<Type, int> TypeDictionary = new Dictionary<Type, int>{
            {typeof(Qr),0},
            {typeof(Pet),1}
        };
    }
}