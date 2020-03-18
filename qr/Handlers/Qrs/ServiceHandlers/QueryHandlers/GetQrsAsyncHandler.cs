﻿using AutoMapper;
using MediatR;
using qrAPI.Domain;
using qrAPI.Queries.Qrs.ServiceQueries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using qrAPI.Data;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Handlers.Qrs.ServiceHandlers.QueryHandlers
{
    public class GetQrsAsyncHandler : IRequestHandler<GetQrsAsyncQuery, IEnumerable<Qr>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<QrDto> _qrRepository;


        public GetQrsAsyncHandler(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            _qrRepository = dataContext.QrRepository;
        }

        public async Task<IEnumerable<Qr>> Handle(GetQrsAsyncQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Qr>>(await _qrRepository.GetAllAsync());
        }
    }
}