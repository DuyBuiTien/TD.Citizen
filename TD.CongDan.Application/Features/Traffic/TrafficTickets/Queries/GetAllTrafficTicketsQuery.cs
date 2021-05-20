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

namespace TD.CongDan.Application.Features.TrafficTickets.Queries
{
    public class GetAllTrafficTicketsQuery : IRequest<PaginatedResult<TrafficTicketsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllTrafficTicketsQuery(int pageNumber, int pageSize,  string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GGetAllQueryHandler : IRequestHandler<GetAllTrafficTicketsQuery, PaginatedResult<TrafficTicketsResponse>>
    {
        private readonly ITrafficTicketRepository _repository;

        public GGetAllQueryHandler(ITrafficTicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<TrafficTicketsResponse>> Handle(GetAllTrafficTicketsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<TrafficTicket, TrafficTicketsResponse>> expression = e => new TrafficTicketsResponse
            {
                Id = e.Id,
                LicensePlateNumber = e.LicensePlateNumber,
                Behaviour = e.Behaviour,
                DateOfOffence = e.DateOfOffence,
                Location = e.Location,
                Device = e.Device,
                Unit = e.Unit,
                PhoneNumber = e.PhoneNumber,
                Price = e.Price,
                Images = e.Images,
                Status = e.Status,
                Description = e.Description
            };

        var paginatedList = await _repository.TrafficTickets
                .Search(request.KeySearch)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
