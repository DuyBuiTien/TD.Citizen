using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.VehicleTypes.Queries
{
    public class GetVehicleTypeByIdQuery : IRequest<Result<VehicleTypesResponse>>
    {
        public int Id { get; set; }

        public class GetPlaceTypeByIdQueryHandler : IRequestHandler<GetVehicleTypeByIdQuery, Result<VehicleTypesResponse>>
        {
            private readonly IVehicleTypeRepository _repository;
            private readonly IMapper _mapper;

            public GetPlaceTypeByIdQueryHandler(IVehicleTypeRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<VehicleTypesResponse>> Handle(GetVehicleTypeByIdQuery query, CancellationToken cancellationToken)
            {
                var category = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<VehicleTypesResponse>(category);
                return Result<VehicleTypesResponse>.Success(mappedCategory);
            }
        }
    }
}