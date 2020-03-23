using System;
using System.Collections.Generic;

namespace qrAPI.Dtos
{
    public static class DtosDictionary
    {
        public static readonly IDictionary<Type, int> TypeDictionary = new Dictionary<Type, int>{
            {typeof(QrDto),0},
            {typeof(PetDto),1}
        };
    }
}
