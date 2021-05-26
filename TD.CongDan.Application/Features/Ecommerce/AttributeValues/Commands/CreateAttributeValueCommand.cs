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

namespace TD.CongDan.Application.Features.AttributeValues.Commands
{
    public partial class CreateAttributeValueCommand : IRequest<Result<int>>
    {
        public string Value { get; set; }
        public int? AttributeId { get; set; }
        public int Position { get; set; }
        public bool IsDefault { get; set; }
        public int Status { get; set; }
    }

    public class CreateCommandHandler : IRequestHandler<CreateAttributeValueCommand, Result<int>>
    {
        private readonly IAttributeValueRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCommandHandler(IAttributeValueRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        [System.Obsolete]
        public async Task<Result<int>> Handle(CreateAttributeValueCommand request, CancellationToken cancellationToken)
        {
            
            var item = _mapper.Map<Domain.Entities.Ecommerce.AttributeValue>(request);

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
