using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Religions.Queries
{
    public class GetAllReligionsQuery : IRequest<Result<List<ReligionsResponse>>>
    {
        public GetAllReligionsQuery()
        {
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllReligionsQuery, Result<List<ReligionsResponse>>>
    {
        private readonly IReligionRepository _repository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IReligionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<ReligionsResponse>>> Handle(GetAllReligionsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _repository.GetListAsync();
            var mappedBrands = _mapper.Map<List<ReligionsResponse>>(brandList);
            return Result<List<ReligionsResponse>>.Success(mappedBrands);
        }
    }
}