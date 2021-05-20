using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.LicensePlates.Queries
{
    public class GetLicensePlateByIdQuery : IRequest<Result<LicensePlatesResponse>>
    {
        public int Id { get; set; }

        public class GetPlaceTypeByIdQueryHandler : IRequestHandler<GetLicensePlateByIdQuery, Result<LicensePlatesResponse>>
        {
            private readonly ILicensePlateRepository _repository;
            private readonly IMapper _mapper;

            public GetPlaceTypeByIdQueryHandler(ILicensePlateRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<LicensePlatesResponse>> Handle(GetLicensePlateByIdQuery query, CancellationToken cancellationToken)
            {
                var category = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<LicensePlatesResponse>(category);
                return Result<LicensePlatesResponse>.Success(mappedCategory);
            }
        }
    }
}