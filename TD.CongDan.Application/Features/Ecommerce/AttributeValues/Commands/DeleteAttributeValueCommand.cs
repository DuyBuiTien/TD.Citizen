using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.AttributeValues.Commands
{
    public class DeleteAttributeValueCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteAreaCommandHandler : IRequestHandler<DeleteAttributeValueCommand, Result<int>>
        {
            private readonly IAttributeValueRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAreaCommandHandler(IAttributeValueRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteAttributeValueCommand command, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
