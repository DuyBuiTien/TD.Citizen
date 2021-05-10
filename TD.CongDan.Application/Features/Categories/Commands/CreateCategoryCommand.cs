using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Categories.Commands
{
    public partial class CreateCategoryCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;
           
            
            var category = _mapper.Map<Category>(request);
            await _categoryRepository.InsertAsync(category);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(category.Id);
        }
    }
}
