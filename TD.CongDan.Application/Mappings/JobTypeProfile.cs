
using AutoMapper;
using TD.CongDan.Application.Features.JobTypes.Commands;
using TD.CongDan.Application.Features.JobTypes.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class JobTypeProfile : Profile
    {
        public JobTypeProfile()
        {
            CreateMap<JobTypesResponse, JobType>().ReverseMap();
            CreateMap<CreateJobTypeCommand, JobType>().ReverseMap();
        }
    }
}