using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.MaritalStatuses.Queries
{
    public class GetAllMaritalStatusesQuery : IRequest<Result<List<MaritalStatusesResponse>>>
    {
        public GetAllMaritalStatusesQuery()
        {
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllMaritalStatusesQuery, Result<List<MaritalStatusesResponse>>>
    {
        private readonly IMaritalStatusRepository _repository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IMaritalStatusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<MaritalStatusesResponse>>> Handle(GetAllMaritalStatusesQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _repository.GetListAsync();
            var mappedBrands = _mapper.Map<List<MaritalStatusesResponse>>(brandList);
            return Result<List<MaritalStatusesResponse>>.Success(mappedBrands);
        }
    }
}