using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Interfaces.Contexts;

namespace TD.CongDan.Application.Features.Recruitments.Queries
{
    public class GetRecruitmentByIdQuery : IRequest<Result<RecruitmentResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetRecruitmentByIdQuery, Result<RecruitmentResponse>>
        {
            private readonly IRecruitmentRepository _repository;
            private readonly IApplicationDbContext _applicationDbContext;

            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IRecruitmentRepository repository, IMapper mapper, IApplicationDbContext applicationDbContext)
            {
                _repository = repository;
                _mapper = mapper;
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Result<RecruitmentResponse>> Handle(GetRecruitmentByIdQuery query, CancellationToken cancellationToken)
            {
                 var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<RecruitmentResponse>(item);
                mappedCategory.CompanyName = item.Company.Name;
                return Result<RecruitmentResponse>.Success(mappedCategory);
            }
        }
    }
}