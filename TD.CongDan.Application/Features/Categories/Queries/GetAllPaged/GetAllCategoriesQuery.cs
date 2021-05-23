using TD.CongDan.Application.Extensions;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.Libs.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Categories.Queries.GetAllPaged
{
    public class GetAllCategoriesQuery : IRequest<PaginatedResult<JobAgesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllCategoriesQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginatedResult<JobAgesResponse>>
    {
        private readonly ICategoryRepository _repository;

        public GGetAllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<JobAgesResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, JobAgesResponse>> expression = e => new JobAgesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
                Icon = e.Icon,
                Image = e.Image,
                CoverImage = e.CoverImage
            };
            var paginatedList = await _repository.Categories
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}