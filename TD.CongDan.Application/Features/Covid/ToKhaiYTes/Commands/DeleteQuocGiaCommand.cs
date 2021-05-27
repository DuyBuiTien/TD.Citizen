using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.ToKhaiYTes.Commands
{
    public class DeleteToKhaiYTeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteToKhaiYTeCommand, Result<int>>
        {
            private readonly IToKhaiYTeRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCommandHandler(IToKhaiYTeRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteToKhaiYTeCommand command, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
