using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.Libs.ThrowR;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Application.Features.Attributes.Commands
{
    public partial class UpdateAttributeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool? IsVisibleOnFront { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsFilterable { get; set; }
        public bool? IsSearchable { get; set; }
        public bool? IsEditable { get; set; }
        public bool? IsSellerEditable { get; set; }
        public string? DefaultValue { get; set; }
        public FrontendInput? FrontendInput { get; set; }
        public DataType? DataType { get; set; }
        public FrontendInput? InputType { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateAttributeCommand, Result<int>>
    {
        private readonly IAttributeRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCategoryCommandHandler(IAttributeRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateAttributeCommand command, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null)
            {
                return Result<int>.Fail($"EcommerceCategory Not Found.");
            }
            else
            {
                item.Code = command.Code ?? item.Code;
                item.DisplayName = command.DisplayName ?? item.DisplayName;
                item.Description = command.Description ?? item.Description;
                item.IsVisibleOnFront = command.IsVisibleOnFront ?? item.IsVisibleOnFront;
                item.IsRequired = command.IsRequired ?? item.IsRequired;
                item.IsFilterable = command.IsFilterable ?? item.IsFilterable;
                item.IsSearchable = command.IsSearchable ?? item.IsSearchable;
                item.IsEditable = command.IsEditable ?? item.IsEditable;
                item.IsSellerEditable = command.IsSellerEditable ?? item.IsSellerEditable;
                item.DefaultValue = command.DefaultValue ?? item.DefaultValue;
                item.FrontendInput = command.FrontendInput ?? item.FrontendInput;
                item.DataType = command.DataType ?? item.DataType;
                item.InputType = command.InputType ?? item.InputType;

                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}

