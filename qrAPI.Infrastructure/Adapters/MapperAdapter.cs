using System;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;

namespace qrAPI.Infrastructure.Adapters
{
    public class MapperAdapter : IMapperAdapter
    {
        private readonly IMapper _mapper;

        public MapperAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<T> DoMapAsync<T>(Func<Task<object>> function)
            => _mapper.Map<T>(await function());

        public async Task<T1> MapDoAsync<T, T1>(object input, Func<T, Task<T1>> function)
        {
            var objectMapped = _mapper.Map<T>(input);
            return await function(objectMapped);
        }

        public async Task<T1> MapDoAsync<T1>(object input, Func<object, Task<T1>> function)
        {
            var objectMappedType = function.GetMethodInfo()?.GetParameters()[0].ParameterType;
            var objectMapped = _mapper.Map(input, objectMappedType, objectMappedType);
            return await function(objectMapped);
        }

        public async Task<T2> MapDoMapAsync<T1, T2>(object input, Func<T1, Task<object>> function)
        {
            var objectMapped = _mapper.Map<T1>(input);
            var objectDone = await function(objectMapped);
            return _mapper.Map<T2>(objectDone);
        }

        public async Task<T1> MapDoMapAsync<T1>(object input, Func<object, Task<object>> function)
        {
            var objectMappedType = function.GetMethodInfo()?.GetParameters()[0].ParameterType;
            var objectMapped = _mapper.Map(input, input.GetType(), objectMappedType);
            var objectDone = await function(objectMapped);
            return _mapper.Map<T1>(objectDone);
        }

    }
}
