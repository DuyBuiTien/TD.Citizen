using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Are.Commands
{
    public partial class UpdateAreaCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string NameWithType { get; set; }
        public string Path { get; set; }
        public string PathWithType { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateAreaCommand, Result<int>>
    {
        private readonly IAreaRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCategoryCommandHandler(IAreaRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateAreaCommand command, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null)
            {
                return Result<int>.Fail($"Area Not Found.");
            }
            else
            {
                item.Name = command.Name ?? item.Name;
                item.Code = command.Code ?? item.Code;
                item.ParentCode = command.ParentCode ?? item.ParentCode;
                item.Slug = command.Slug ?? item.Slug;
                item.Type = command.Type ?? item.Type;
                item.NameWithType = command.NameWithType ?? item.NameWithType;
                item.Path = command.Path ?? item.Path;
                item.PathWithType = command.PathWithType ?? item.PathWithType;
                item.Description = command.Description ?? item.Description;

                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}

