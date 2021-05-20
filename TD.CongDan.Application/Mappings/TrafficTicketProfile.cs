
using AutoMapper;
using TD.CongDan.Application.Features.Genders.Queries;
using TD.CongDan.Application.Features.Salaries.Commands;
using TD.CongDan.Application.Features.Salaries.Queries;
using TD.CongDan.Application.Features.TrafficTickets.Queries;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Mappings
{
    internal class TrafficTicketProfile : Profile
    {
        public TrafficTicketProfile()
        {
            CreateMap<TrafficTicketsResponse, TrafficTicket>().ReverseMap();
        }
    }
}