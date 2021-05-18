﻿using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.Libs.ThrowR;
using TD.CongDan.Domain.Entities.Company;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TD.CongDan.Application.Features.Companies.Queries
{
    public class GetCompanyByIdQuery : IRequest<Result<CompanyResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Result<CompanyResponse>>
        {
            private readonly ICompanyRepository _repository;
            private readonly IPlaceRepository _placeRepository;
            private readonly IIndustryRepository _industryRepository;
            private readonly ICompanyIndustryRepository _companyIndustryRepository;
            //private readonly IRepositoryAsync<Company> _repositoryAsync;

            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IIndustryRepository industryRepository, ICompanyIndustryRepository companyIndustryRepository, ICompanyRepository repository, IPlaceRepository placeRepository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
                _placeRepository = placeRepository;
                _companyIndustryRepository = companyIndustryRepository;
                _industryRepository = industryRepository;
            }

            public async Task<Result<CompanyResponse>> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                Throw.Exception.IfNull(item, "Company", "No Company Found");
                var placeId = item.PlaceId;
                Place place = await _placeRepository.GetByIdAsync((int)placeId);

                var mappedCategory = _mapper.Map<CompanyResponse>(item);
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

                Expression<Func<CompanyIndustry, int>> expression = e => (int)e.IndustryId;
                var listIdIndustry = _companyIndustryRepository.CompanyIndustries.Where(x => x.CompanyId == item.Id).Select(expression).ToList() ;

                ICollection<Industry> list = new List<Industry>();

                foreach (var tmp in listIdIndustry)
                {
                    var item_industry = await _industryRepository.GetByIdAsync(tmp);
                    if (item_industry!=null)
                    {
                        list.Add(item_industry);
                    }
                }

                mappedCategory.Industries = list;

                return Result<CompanyResponse>.Success(mappedCategory);
            }
        }
    }
}