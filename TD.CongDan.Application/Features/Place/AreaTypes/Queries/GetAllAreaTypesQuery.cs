using TD.Libs.Results;
using AutoMapper;
using MediatR;

using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.AreaTypes.Queries
{
    public class GetAllAreaTypesQuery : IRequest<Result<List<AreaTypesResponse>>>
    {
        public GetAllAreaTypesQuery()
        {
        }
    }

    public class GetAllBrandsCachedQueryHandler : IRequestHandler<GetAllAreaTypesQuery, Result<List<AreaTypesResponse>>>
    {
        private readonly IAreaTypeRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBrandsCachedQueryHandler(IAreaTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<AreaTypesResponse>>> Handle(GetAllAreaTypesQuery request, CancellationToken cancellationToken)
        {
            var areaTypeList = await _repository.GetListAsync();
            var mappedAreaTypes = _mapper.Map<List<AreaTypesResponse>>(areaTypeList);
            return Result<List<AreaTypesResponse>>.Success(mappedAreaTypes);
        }
    }
}
