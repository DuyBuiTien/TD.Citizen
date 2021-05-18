
using AutoMapper;
using TD.CongDan.Application.Features.JobApplieds.Queries;
using TD.CongDan.Application.Features.JobSaveds.Queries;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class JobAppliedProfile : Profile
    {
        public JobAppliedProfile()
        {
            CreateMap<JobAppliedsResponse, JobApplied>().ReverseMap();
        }
    }
}