using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using qrAPI.App.Domain;
using qrAPI.App.Services.Interfaces;
using qrAPI.App.Validations;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.App.Services.Implementations
{
    public abstract partial class AbstractGenericService<TDomainObject, TDto> : IGenericService<TDomainObject>
    where TDomainObject : DomainObject
    where TDto : Dto
    {
        protected readonly IMapper Mapper;
        private readonly IRepository<TDto> _repository;
        private readonly ILogger<AbstractGenericService<TDomainObject, TDto>> _logger;
        private readonly AbstractValidator<TDomainObject> _validator;

        protected AbstractGenericService(
            IMapper mapper,
            IRepository<TDto> repository,
            ILogger<AbstractGenericService<TDomainObject, TDto>> logger,
            AbstractValidator<TDomainObject> validator)
        {
            Mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }

        public virtual async Task<IEnumerable<TDomainObject>> GetAllAsync() =>
            Mapper.Map<IEnumerable<TDomainObject>>(await _repository.GetAllAsync());

        public virtual Task<TDomainObject> GetByIdAsync(Guid id) =>
        TryCatch(async () =>
        {
            CommonValidations.ValidateId(id);
            return Mapper.Map<TDomainObject>(await _repository.GetAsync(id));
        });

        public virtual async Task<TDomainObject> CreateAsync(TDomainObject objToCreate) =>
            Mapper.Map<TDomainObject>(await _repository.InsertAsync(Mapper.Map<TDto>(objToCreate)));

        public virtual Task<bool> UpdateAsync(Guid id, TDomainObject objToUpdate) =>
        TryCatch(async () =>
        {
            CommonValidations.ValidateId(id);
            objToUpdate.Id = id;
            await _validator.ValidateAndThrowAsync(objToUpdate);
            return await _repository.UpdateAsync(Mapper.Map<TDto>(objToUpdate));
        });

        public virtual Task<bool> DeleteAsync(Guid id) =>
        TryCatch(async () =>
        {
            CommonValidations.ValidateId(id);
            return await _repository.DeleteAsync(id);
        });
    }
}