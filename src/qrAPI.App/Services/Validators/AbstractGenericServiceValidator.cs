using FluentValidation;
using Microsoft.Extensions.Logging;
using qrAPI.App.Domain;
using qrAPI.App.Services.Interfaces;
using qrAPI.Infra.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace qrAPI.App.Services.Validators
{
    public class AbstractGenericServiceValidator<TDomainObject> : IGenericService<TDomainObject>
        where TDomainObject : DomainObject
    {
        public readonly IGenericService<TDomainObject> _decoratedService;
        private readonly AbstractValidator<TDomainObject> _validator;
        public readonly ILogger<AbstractGenericServiceValidator<TDomainObject>> _logger;

        public AbstractGenericServiceValidator(
            ILogger<AbstractGenericServiceValidator<TDomainObject>> logger,
            IGenericService<TDomainObject> decoratedService,
            AbstractValidator<TDomainObject> validator)
        {
            _logger = logger;
            _decoratedService = decoratedService;
            _validator = validator;
        }

        public async Task<IEnumerable<TDomainObject>> GetAllAsync() =>
            await _decoratedService.GetAllAsync();

        public async Task<TDomainObject> GetByIdAsync(Guid id) =>
            await _decoratedService.GetByIdAsync(id);

        public async Task<TDomainObject> CreateAsync(TDomainObject obj) =>
            await TryCatchFuncTDomainObj(obj,
                async (obje) => {
                    return await _decoratedService.CreateAsync(obje);
                });

        public async Task<bool> UpdateAsync(Guid id, TDomainObject objToUpdate) =>
            await _decoratedService.UpdateAsync(id, objToUpdate);

        public async Task<bool> DeleteAsync(Guid id) =>
            await _decoratedService.DeleteAsync(id);

        /// <summary>
        /// Try to execute a block of code, and handle the possible exceptions of it.
        /// </summary>
        /// <code language="c#">
        /// TryCatch(async () =>
        /// {
        ///     await _validator.ValidateAndThrowAsync(objToUpdate);
        ///     return await _repository.UpdateAsync(Mapper.Map&lt;TDto&gt;(objToUpdate));
        /// });
        /// </code>
        /// <param name="deleteFunction">Block of code to run</param>
        private async Task<T> TryCatchFuncTDomainObj<T>(TDomainObject obj, Func<TDomainObject, Task<T>> func)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(obj);
                return await func.Invoke(obj);
            }
            catch (InvalidIdException invalidIdException)
            {
                _logger.LogError(invalidIdException, "Invalid Id");
                Console.WriteLine("The Id is invalid");
                throw;
            }
            catch (IdNotFoundException idNotFoundException)
            {
                _logger.LogError(idNotFoundException, "Id not found");
                Console.WriteLine("The id is not found.");
                throw;
            }
            catch (DbDeleteException dbDeleteException)
            {
                _logger.LogError(dbDeleteException, "Database error");
                Console.WriteLine("The was an error when trying to delete.");
                throw;
            }
            catch (ValidationException validationException)
            {
                _logger.LogError(validationException, "Validation Exception");
                Console.WriteLine("There has been a validation exception.");
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unhandled Exception");
                Console.WriteLine("There has been an exception");
                throw;
            }
        }
    }
}