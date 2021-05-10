using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.PlaceTypes.Queries
{
    public class GetPlaceByIdQuery : IRequest<Result<PlaceTypesResponse>>
    {
        public int Id { get; set; }

        public class GetPlaceTypeByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, Result<PlaceTypesResponse>>
        {
            private readonly IPlaceTypeRepository _repository;
            private readonly IMapper _mapper;

            public GetPlaceTypeByIdQueryHandler(IPlaceTypeRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<PlaceTypesResponse>> Handle(GetPlaceByIdQuery query, CancellationToken cancellationToken)
            {
                var category = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<PlaceTypesResponse>(category);
                return Result<PlaceTypesResponse>.Success(mappedCategory);
            }
        }
    }
}