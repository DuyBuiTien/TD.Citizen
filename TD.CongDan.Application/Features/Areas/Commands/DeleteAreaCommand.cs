using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Are.Commands
{
    public class DeleteAreaCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand, Result<int>>
        {
            private readonly IAreaRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAreaCommandHandler(IAreaRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteAreaCommand command, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
