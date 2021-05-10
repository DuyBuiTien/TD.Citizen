using TD.Libs.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Places.Commands
{
    public class DeletePlaceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePlaceTypeCommandHandler : IRequestHandler<DeletePlaceCommand, Result<int>>
        {
            private readonly IPlaceRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeletePlaceTypeCommandHandler(IPlaceRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeletePlaceCommand command, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
