using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.TrafficTickets.Queries
{
    public class GetTrafficTicketByIdQuery : IRequest<Result<TrafficTicketsResponse>>
    {
        public int Id { get; set; }

        public class GetPlaceTypeByIdQueryHandler : IRequestHandler<GetTrafficTicketByIdQuery, Result<TrafficTicketsResponse>>
        {
            private readonly IPlaceTypeRepository _repository;
            private readonly IMapper _mapper;

            public GetPlaceTypeByIdQueryHandler(IPlaceTypeRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<TrafficTicketsResponse>> Handle(GetTrafficTicketByIdQuery query, CancellationToken cancellationToken)
            {
                var category = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<TrafficTicketsResponse>(category);
                return Result<TrafficTicketsResponse>.Success(mappedCategory);
            }
        }
    }
}