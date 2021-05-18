using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;

namespace TD.CongDan.Application.Features.JobApplieds.Commands
{
    public class DeleteJobAppliedCommand : IRequest<Result<int>>
    {
        public int RecruitmentId { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteJobAppliedCommand, Result<int>>
        {
            private readonly IJobAppliedRepository _repository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAuthenticatedUserService _authenticatedUser;


            public DeleteCommandHandler(IJobAppliedRepository repository, IUnitOfWork unitOfWork, IAuthenticatedUserService _authenticatedUser)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                this._authenticatedUser = _authenticatedUser;
            }

            public async Task<Result<int>> Handle(DeleteJobAppliedCommand command, CancellationToken cancellationToken)
            {
                var userName = _authenticatedUser.Username;
                var item = await _repository.GetByIdAsync(userName, command.RecruitmentId);
                await _repository.DeleteAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
