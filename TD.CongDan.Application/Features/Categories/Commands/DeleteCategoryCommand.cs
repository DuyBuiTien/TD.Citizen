using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<int>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
            {
                _categoryRepository = categoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetByIdAsync(command.Id);
                await _categoryRepository.DeleteAsync(category);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(category.Id);
            }
        }
    }
}
