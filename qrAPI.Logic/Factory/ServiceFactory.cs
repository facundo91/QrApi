using System;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services;

namespace qrAPI.Logic.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IGenericService<T> GetService<T>() where T : DomainObject =>
            ServicesDictionary.TypeDictionary[typeof(T)] switch
            {
                0 => (IGenericService<T>)_serviceProvider.GetService(typeof(IQrService)),
                1 => (IGenericService<T>)_serviceProvider.GetService(typeof(IPetService)),
                _ => throw new InvalidOperationException()
            };

        public IMedicalRecordService GetMedicalRecordService()
            => (IMedicalRecordService) _serviceProvider.GetService(typeof(IMedicalRecordService));

    }
}