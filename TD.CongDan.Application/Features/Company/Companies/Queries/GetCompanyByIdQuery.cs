using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.Libs.ThrowR;

namespace TD.CongDan.Application.Features.Companies.Queries
{
    public class GetCompanyByIdQuery : IRequest<Result<CompaniesResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Result<CompaniesResponse>>
        {
            private readonly ICompanyRepository _repository;
            private readonly IPlaceRepository _placeRepository;

            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(ICompanyRepository repository, IPlaceRepository placeRepository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
                _placeRepository = placeRepository;
            }

            public async Task<Result<CompaniesResponse>> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                Throw.Exception.IfNull(item, "Company", "No Company Found");
                var placeId = item.PlaceId;
                Place place = await _placeRepository.GetByIdAsync((int)placeId);

                var mappedCategory = _mapper.Map<CompaniesResponse>(item);
                if (place!=null)
                {
                    mappedCategory.PlaceName = place.PlaceName;
                    mappedCategory.ProvinceId = place.ProvinceId;
                    mappedCategory.Province = place.Province.NameWithType;
                    mappedCategory.District = place.District.NameWithType;
                    mappedCategory.DistrictId = place.DistrictId;
                    mappedCategory.CommuneId = place.CommuneId;
                    mappedCategory.Commune = place.Commune.NameWithType;
                    mappedCategory.Latitude = place.Latitude;
                    mappedCategory.Longitude = place.Longitude;
                }
                

                return Result<CompaniesResponse>.Success(mappedCategory);
            }
        }
    }
}