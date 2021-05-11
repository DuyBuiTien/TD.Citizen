using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.PlaceTypes.Commands
{
    public partial class CreatePlaceTypeCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
    }

    public class CreatePlaceTypeCommandHandler : IRequestHandler<CreatePlaceTypeCommand, Result<int>>
    {
        private readonly IPlaceTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePlaceTypeCommandHandler(IPlaceTypeRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreatePlaceTypeCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;
           
            var item = _mapper.Map<PlaceType>(request);
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
