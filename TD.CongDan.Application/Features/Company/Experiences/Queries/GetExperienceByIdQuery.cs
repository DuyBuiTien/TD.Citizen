using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Features.JobAges.Queries;

namespace TD.CongDan.Application.Features.Experiences.Queries
{
    public class GetExperienceByIdQuery : IRequest<Result<ExperiencesResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetExperienceByIdQuery, Result<ExperiencesResponse>>
        {
            private readonly IExperienceRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IExperienceRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<ExperiencesResponse>> Handle(GetExperienceByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<ExperiencesResponse>(item);
                return Result<ExperiencesResponse>.Success(mappedCategory);
            }
        }
    }
}