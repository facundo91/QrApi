using System;
using System.Threading.Tasks;

namespace qrAPI.Infrastructure.Adapters
{
    public interface IMapperAdapter
    {
        Task<T> DoMapAsync<T>(Func<Task<object>> function);
        Task<T1> MapDoAsync<T1>(object input, Func<object, Task<T1>> function);
        Task<T1> MapDoAsync<T, T1>(object input, Func<T, Task<T1>> function);
        Task<T2> MapDoMapAsync<T1, T2>(object input, Func<T1, Task<object>> function);
        Task<T1> MapDoMapAsync<T1>(object input, Func<object, Task<object>> function);
    }
}