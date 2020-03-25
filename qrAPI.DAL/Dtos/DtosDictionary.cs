using System;
using System.Collections.Generic;

namespace qrAPI.DAL.Dtos
{
    public static class DtosDictionary
    {
        public static readonly IDictionary<Type, int> TypeDictionary = new Dictionary<Type, int>{
            {typeof(QrDto),0},
            {typeof(PetDto),1},
            {typeof(RefreshToken),2}
        };
    }
}
