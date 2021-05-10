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
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Categories.Commands
{
    public partial class UpdateCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.Id);

            if (category == null)
            {
                return Result<int>.Fail($"Category Not Found.");
            }
            else
            {
                category.Name = command.Name ?? category.Name;
                category.Code = command.Code ?? category.Code;
                category.Description = command.Description ?? category.Description;
                await _categoryRepository.UpdateAsync(category);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(category.Id);
            }
        }
    }
}
