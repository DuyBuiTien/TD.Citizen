using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Interfaces.Contexts;

namespace TD.CongDan.Application.Features.Carpools.Queries
{
    public class GetCarpoolByIdQuery : IRequest<Result<CarpoolResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCarpoolByIdQuery, Result<CarpoolResponse>>
        {
            private readonly ICarpoolRepository _repository;
            private readonly IApplicationDbContext _applicationDbContext;

            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(ICarpoolRepository repository, IMapper mapper, IApplicationDbContext applicationDbContext)
            {
                _repository = repository;
                _mapper = mapper;
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Result<CarpoolResponse>> Handle(GetCarpoolByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<CarpoolResponse>(item);
                return Result<CarpoolResponse>.Success(mappedCategory);
            }
        }
    }
}