using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Features.VehicleTypes.Commands
{
    public partial class CreateVehicleTypeCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public int SeatCount { get; set; }
        public string Description { get; set; }
    }

    public class CreatePlaceTypeCommandHandler : IRequestHandler<CreateVehicleTypeCommand, Result<int>>
    {
        private readonly IVehicleTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePlaceTypeCommandHandler(IVehicleTypeRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;
           
            var item = _mapper.Map<VehicleType>(request);
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
