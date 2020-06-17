using FluentValidation;
using Microsoft.Extensions.Logging;
using qrAPI.Infra.Exceptions;
using System;
using System.Threading.Tasks;

namespace qrAPI.App.Services.Implementations
{
    public abstract partial class AbstractGenericService<TDomainObject, TDto>
    {
        private delegate Task<bool> DeleteFunction();
        private delegate Task<TDomainObject> GetFunction();

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
        private async Task<bool> TryCatch(DeleteFunction deleteFunction)
        {
            try
            {
                return await deleteFunction();
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

        private async Task<TDomainObject> TryCatch(GetFunction getFunction)
        {
            try
            {
                return await getFunction();
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
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unhandled Exception");
                Console.WriteLine("There has been an exception");
                throw;
            }
        }

        //private static async Task<T> TryCatch<T>(Func<Task<T>> func)
        //{
        //    try
        //    {
        //        return await func.Invoke();
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine("There has been an exception");
        //        throw;
        //    }
        //}
    }
}