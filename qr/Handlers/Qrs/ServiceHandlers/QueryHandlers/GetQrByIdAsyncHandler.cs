﻿using AutoMapper;
using MediatR;
using qrAPI.Domain;
using qrAPI.Queries.Qrs.ServiceQueries;
using System.Threading;
using System.Threading.Tasks;
using qrAPI.Data;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Handlers.Qrs.ServiceHandlers.QueryHandlers
{
    public class GetQrByIdAsyncHandler : IRequestHandler<GetQrByIdAsyncQuery, Qr>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<QrDto> _qrRepository;


        public GetQrByIdAsyncHandler(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            _qrRepository = dataContext.QrRepository;
        }

        public async Task<Qr> Handle(GetQrByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<Qr>(await _qrRepository.GetByIdAsync(request.Id));
        }
    }
}