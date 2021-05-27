using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;

using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Application.Features.Attributes.Commands
{
    public partial class CreateAttributeCommand : IRequest<Result<int>>
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsVisibleOnFront { get; set; }
        public bool IsRequired { get; set; }
        public bool IsFilterable { get; set; }
        public bool IsSearchable { get; set; }
        public bool IsEditable { get; set; }
        public bool IsSellerEditable { get; set; }
        public string DefaultValue { get; set; }
        public FrontendInput FrontendInput { get; set; }
        public DataType DataType { get; set; }
        public FrontendInput InputType { get; set; }
    }

    public class CreateCommandHandler : IRequestHandler<CreateAttributeCommand, Result<int>>
    {
        private readonly IAttributeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCommandHandler(IAttributeRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        [System.Obsolete]
        public async Task<Result<int>> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
        {
            
            var item = _mapper.Map<Domain.Entities.Ecommerce.Attribute>(request);

            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);

        }
        public static string convert(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }



}
