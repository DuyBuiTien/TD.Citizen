
using AutoMapper;
using TD.CongDan.Application.Features.JobApplications.Commands;
using TD.CongDan.Application.Features.JobApplications.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class JobApplicationProfile : Profile
    {
        public JobApplicationProfile()
        {
            CreateMap<JobApplicationsResponse, JobApplication>().ReverseMap();
            CreateMap<JobApplicationResponse, JobApplication>().ReverseMap();
            CreateMap<CreateJobApplicationCommand, JobApplication>().ReverseMap();
        }
    }
}