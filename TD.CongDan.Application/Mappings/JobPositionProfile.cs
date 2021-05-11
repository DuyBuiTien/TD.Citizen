
using AutoMapper;
using TD.CongDan.Application.Features.JobPositions.Commands;
using TD.CongDan.Application.Features.JobPositions.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class JobPositionProfile : Profile
    {
        public JobPositionProfile()
        {
            CreateMap<JobPositionsResponse, JobPosition>().ReverseMap();
            CreateMap<CreateJobPositionCommand, JobPosition>().ReverseMap();
        }
    }
}