using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.EcommerceCategories.Commands
{
    public class DeleteEcommerceCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteAreaCommandHandler : IRequestHandler<DeleteEcommerceCategoryCommand, Result<int>>
        {
            private readonly IEcommerceCategoryRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAreaCommandHandler(IEcommerceCategoryRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteEcommerceCategoryCommand command, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
