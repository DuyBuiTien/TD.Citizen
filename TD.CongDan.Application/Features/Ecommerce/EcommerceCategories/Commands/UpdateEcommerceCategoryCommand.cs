using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.Libs.ThrowR;

namespace TD.CongDan.Application.Features.EcommerceCategories.Commands
{
    public partial class UpdateEcommerceCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int? Position { get; set; }
        public bool? IncludeInMenu { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string[] Tags { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateEcommerceCategoryCommand, Result<int>>
    {
        private readonly IEcommerceCategoryRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCategoryCommandHandler(IEcommerceCategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateEcommerceCategoryCommand command, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null)
            {
                return Result<int>.Fail($"EcommerceCategory Not Found.");
            }
            else
            {

                if (command.ParentId == null)
                {
                    item.Level = 1;
                }
                else
                {
                    var category = await _repository.GetByIdAsync((int)command.ParentId);
                    Throw.Exception.IfNull(category, "EcommerceCategory", "No EcommerceCategory ParentId Found");
                    item.Level = category.Level + 1;

                }

                item.Name = command.Name ?? item.Name;
                item.ParentId = command.ParentId ?? item.ParentId;
                item.Slug = command.Slug ?? item.Slug;
                item.Description = command.Description ?? item.Description;
                item.MetaTitle = command.MetaTitle ?? item.MetaTitle;
                item.MetaDescription = command.MetaDescription ?? item.MetaDescription;
                item.Position = command.Position ?? item.Position;
                item.IncludeInMenu = command.IncludeInMenu ?? item.IncludeInMenu;
                item.Position = command.Position ?? item.Position;
                item.Icon = command.Icon ?? item.Icon;
                item.Image = command.Image ?? item.Image;
                item.Tags = command.Tags ?? item.Tags;

                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}

