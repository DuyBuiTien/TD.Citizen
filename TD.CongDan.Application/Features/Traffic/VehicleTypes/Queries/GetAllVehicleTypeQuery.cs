using TD.Libs.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Extensions;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Features.VehicleTypes.Queries
{
    public class GetAllVehicleTypeQuery : IRequest<PaginatedResult<VehicleTypesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
       

        public GetAllVehicleTypeQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllQueryHandler : IRequestHandler<GetAllVehicleTypeQuery, PaginatedResult<VehicleTypesResponse>>
    {
        private readonly IVehicleTypeRepository _repository;

        public GGetAllQueryHandler(IVehicleTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<VehicleTypesResponse>> Handle(GetAllVehicleTypeQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<VehicleType, VehicleTypesResponse>> expression = e => new VehicleTypesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                Icon = e.Icon,
                Description = e.Description,
              SeatCount = e.SeatCount
            };


            var paginatedList = await _repository.VehicleTypes
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
