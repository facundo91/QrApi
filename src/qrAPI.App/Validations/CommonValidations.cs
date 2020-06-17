using qrAPI.Infra.Exceptions;
using System;

namespace qrAPI.App.Validations
{
    public static class CommonValidations
    {
        public static void ValidateId(Guid id)
        {
            if (id == null)
                throw new InvalidIdException("Id cannot be null");
        }
    }
}
