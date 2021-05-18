
using AutoMapper;

using TD.CongDan.Application.Features.JobSaveds.Queries;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class JobSavedProfile : Profile
    {
        public JobSavedProfile()
        {
            CreateMap<JobSavedsResponse, JobSaved>().ReverseMap();
        }
    }
}