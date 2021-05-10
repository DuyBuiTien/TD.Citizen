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

namespace TD.CongDan.Application.Features.Degrees.Commands
{
    public partial class UpdateDegreeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateDegreeCommand, Result<int>>
    {
        private readonly IDegreeRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(IDegreeRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateDegreeCommand command, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null)
            {
                return Result<int>.Fail($"JobAge Not Found.");
            }
            else
            {
                item.Name = command.Name ?? item.Name;
                item.Code = command.Code ?? item.Code;
                item.Description = command.Description ?? item.Description;
                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
