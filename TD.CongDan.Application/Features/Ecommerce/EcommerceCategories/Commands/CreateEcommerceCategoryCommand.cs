using TD.Libs.Results;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;
using RestSharp;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.Libs.ThrowR;

namespace TD.CongDan.Application.Features.EcommerceCategories.Commands
{
    public partial class CreateEcommerceCategoryCommand : IRequest<Result<int>>
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int Position { get; set; }
        public bool IncludeInMenu { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string[] Tags { get; set; }
    }

    public class CreateAreaCommandHandler : IRequestHandler<CreateEcommerceCategoryCommand, Result<int>>
    {
        private readonly IEcommerceCategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateAreaCommandHandler(IEcommerceCategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        [System.Obsolete]
        public async Task<Result<int>> Handle(CreateEcommerceCategoryCommand request, CancellationToken cancellationToken)
        {
            
            var item = _mapper.Map<EcommerceCategory>(request);

            if (request.ParentId == null)
            {
                item.Level = 1;
            } else
            {
                var category = await _repository.GetByIdAsync((int)request.ParentId);
                Throw.Exception.IfNull(category, "EcommerceCategory", "No EcommerceCategory ParentId Found");
                item.Level = category.Level + 1;

            }

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
