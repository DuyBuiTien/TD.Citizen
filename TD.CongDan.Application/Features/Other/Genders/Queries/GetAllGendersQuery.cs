using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Genders.Queries
{
    public class GetAllGendersQuery : IRequest<Result<List<GendersResponse>>>
    {
        public GetAllGendersQuery()
        {
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllGendersQuery, Result<List<GendersResponse>>>
    {
        private readonly IGenderRepository _repository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IGenderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<GendersResponse>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _repository.GetListAsync();
            var mappedBrands = _mapper.Map<List<GendersResponse>>(brandList);
            return Result<List<GendersResponse>>.Success(mappedBrands);
        }
    }
}