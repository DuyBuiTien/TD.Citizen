using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Places.Queries
{
    public class GetPlaceByIdQuery : IRequest<Result<PlaceResponse>>
    {
        public int Id { get; set; }

        public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, Result<PlaceResponse>>
        {
            private readonly IPlaceRepository _repository;
            private readonly IMapper _mapper;

            public GetPlaceByIdQueryHandler(IPlaceRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<PlaceResponse>> Handle(GetPlaceByIdQuery query, CancellationToken cancellationToken)
            {
                var category = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<PlaceResponse>(category);
                return Result<PlaceResponse>.Success(mappedCategory);
            }
        }
    }
}