
using AutoMapper;
using TD.CongDan.Application.Features.Are.Commands;
using TD.CongDan.Application.Features.Are.Queries;
using TD.CongDan.Application.Features.Categories.Queries.GetAllPaged;
using TD.CongDan.Application.Features.JobAges.Commands;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class JobAgeProfile : Profile
    {
        public JobAgeProfile()
        {
            CreateMap<JobAgesResponse, JobAge>().ReverseMap();
            CreateMap<CreateJobAgeCommand, JobAge>().ReverseMap();
        }
    }
}