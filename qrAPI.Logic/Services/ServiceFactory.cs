using System;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IPetService<Pet> _petService;
        private readonly IQrService<Qr> _qrService;

        public ServiceFactory(IPetService<Pet> petService, IQrService<Qr> qrService)
        {
            _petService = petService;
            _qrService = qrService;
        }

        public IGenericService<T> GetService<T>() where T : DomainObject
        {
            return ServicesDictionary.TypeDictionary[typeof(T)] switch
            {
                0 => (IGenericService<T>)_qrService,
                1 => (IGenericService<T>)_petService,
                _ => throw new InvalidOperationException()
            };
        }
    }
}
