using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.IdentityTypes.Queries
{
    public class GetAllIdentityTypesQuery : IRequest<Result<List<IdentityTypesResponse>>>
    {
        public GetAllIdentityTypesQuery()
        {
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllIdentityTypesQuery, Result<List<IdentityTypesResponse>>>
    {
        private readonly IIdentityTypeRepository _repository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IIdentityTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<IdentityTypesResponse>>> Handle(GetAllIdentityTypesQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _repository.GetListAsync();
            var mappedBrands = _mapper.Map<List<IdentityTypesResponse>>(brandList);
            return Result<List<IdentityTypesResponse>>.Success(mappedBrands);
        }
    }
}