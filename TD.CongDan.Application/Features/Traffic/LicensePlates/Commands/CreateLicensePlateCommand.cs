using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;
using System;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Features.LicensePlates.Commands
{
    public partial class CreateLicensePlateCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        //Ten chu xe
        public string OwnerFullName { get; set; }
        //Bien so dang ky
        public string LicensePlateNumber { get; set; }
        //Ngay dang ky
        public DateTime? DateOfRegistration { get; set; }

        public string Description { get; set; }
    }

    public class CreatePlaceTypeCommandHandler : IRequestHandler<CreateLicensePlateCommand, Result<int>>
    {
        private readonly ILicensePlateRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePlaceTypeCommandHandler(ILicensePlateRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreateLicensePlateCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.Username;
           
            var item = _mapper.Map<LicensePlate>(request);
            item.UserName = id;
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
