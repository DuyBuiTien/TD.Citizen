using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.Libs.ThrowR;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Application.Features.AttributeValues.Commands
{
    public partial class UpdateAttributeValueCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? AttributeId { get; set; }
        public int? Position { get; set; }
        public bool? IsDefault { get; set; }
        public int? Status { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateAttributeValueCommand, Result<int>>
    {
        private readonly IAttributeValueRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCategoryCommandHandler(IAttributeValueRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateAttributeValueCommand command, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null)
            {
                return Result<int>.Fail($"EcommerceCategory Not Found.");
            }
            else
            {
                item.Value = command.Value ?? item.Value;
                item.AttributeId = command.AttributeId ?? item.AttributeId;
                item.Position = command.Position ?? item.Position;
                item.IsDefault = command.IsDefault ?? item.IsDefault;
                item.Status = command.Status ?? item.Status;


                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}

