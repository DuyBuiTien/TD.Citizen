
using AutoMapper;
using TD.CongDan.Application.Features.JobNames.Commands;
using TD.CongDan.Application.Features.JobNames.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class JobNameProfile : Profile
    {
        public JobNameProfile()
        {
            CreateMap<JobNamesResponse, JobName>().ReverseMap();
            CreateMap<CreateJobNameCommand, JobName>().ReverseMap();
        }
    }
}