using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.Libs.ThrowR;
using TD.CongDan.Application.Interfaces.Shared;
using System.Linq.Expressions;
using System;
using TD.CongDan.Domain.Entities.Company;
using System.Collections.Generic;
using System.Linq;

namespace TD.CongDan.Application.Features.Companies.Queries
{
    public class GetCurrentCompanyQuery : IRequest<Result<CompanyResponse>>
    {
        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCurrentCompanyQuery, Result<CompanyResponse>>
        {
            private readonly ICompanyRepository _repository;
            private readonly IPlaceRepository _placeRepository;
            private readonly IAuthenticatedUserService _authenticatedUser;
            private readonly IMapper _mapper;
            private readonly IIndustryRepository _industryRepository;
            private readonly ICompanyIndustryRepository _companyIndustryRepository;

            public GetCategoryByIdQueryHandler(IIndustryRepository industryRepository, ICompanyIndustryRepository companyIndustryRepository, ICompanyRepository repository, IAuthenticatedUserService authenticatedUser, IPlaceRepository placeRepository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
                _placeRepository = placeRepository;
                _authenticatedUser = authenticatedUser;
                _companyIndustryRepository = companyIndustryRepository;
                _industryRepository = industryRepository;
            }

            public async Task<Result<CompanyResponse>> Handle(GetCurrentCompanyQuery query, CancellationToken cancellationToken)
            {

                var id = _authenticatedUser.Username;

                var item = await _repository.GetByUserNameAsync(id);
                Throw.Exception.IfNull(item, "Company", "No Company Found");
                var mappedCategory = _mapper.Map<CompanyResponse>(item);


                Expression<Func<CompanyIndustry, int>> expression = e => (int)e.IndustryId;
                var listIdIndustry = _companyIndustryRepository.CompanyIndustries.Where(x => x.CompanyId == item.Id).Select(expression).ToList();

                ICollection<Industry> list = new List<Industry>();

                foreach (var tmp in listIdIndustry)
                {
                    var item_industry = await _industryRepository.GetByIdAsync(tmp);
                    if (item_industry != null)
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