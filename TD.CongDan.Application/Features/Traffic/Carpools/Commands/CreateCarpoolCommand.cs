using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;
using System.Globalization;
using TD.CongDan.Application.Features.Places.Commands;
using TD.Libs.ThrowR;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Features.Carpools.Commands
{
    public partial class CreateCarpoolCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime? DepartureDate { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        //Loai phuong tien
        public int? VehicleTypeId
        {
            get; set;
        }

        public string Role { get; set; }
        //Gia
        public decimal Price { get; set; }
        //So ghe
        public int? SeatCount { get; set; }
        //Trang thai

        public string DeparturePlaceName { get; set; }
        public double? DepartureLatitude { get; set; }
        public double? DepartureLongitude { get; set; }
        public int? DepartureProvinceId { get; set; }
        public int? DepartureDistrictId { get; set; }
        public int? DepartureCommuneId { get; set; }


        public string ArrivalPlaceName { get; set; }
        public double? ArrivalLatitude { get; set; }
        public double? ArrivalLongitude { get; set; }
        public int? ArrivalProvinceId { get; set; }
        public int? ArrivalDistrictId { get; set; }
        public int? ArrivalCommuneId { get; set; }

    }


    public class CreateCommandHandler : IRequestHandler<CreateCarpoolCommand, Result<int>>
    {
        private readonly ICarpoolRepository _repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IRecruitmentBenefitRepository _recruitmentBenefitRepository;

        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCommandHandler(ICarpoolRepository repository, IPlaceRepository placeRepository, IRecruitmentBenefitRepository recruitmentBenefitRepository, ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _placeRepository = placeRepository;
            _companyRepository = companyRepository;
            _recruitmentBenefitRepository = recruitmentBenefitRepository;
        }

        public async Task<Result<int>> Handle(CreateCarpoolCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.Username;

            CreatePlaceCommand placeDeparture = new CreatePlaceCommand { 
                PlaceName = request.DeparturePlaceName, 
                ProvinceId = request.DepartureProvinceId, 
                DistrictId = request.DepartureDistrictId, 
                CommuneId = request.DepartureCommuneId, 
                PlaceTypeId = 24, 
                Latitude = request.DepartureLatitude, 
                Longitude = request.DepartureLongitude
            };
            var itemPlaceDeparture = _mapper.Map<Place>(placeDeparture);

            await _placeRepository.InsertAsync(itemPlaceDeparture);
            await _unitOfWork.Commit(cancellationToken);

            CreatePlaceCommand placeArrival = new CreatePlaceCommand
            {
                PlaceName = request.ArrivalPlaceName,
                ProvinceId = request.ArrivalProvinceId,
                DistrictId = request.ArrivalDistrictId,
                CommuneId = request.ArrivalCommuneId,
                PlaceTypeId = 24,
                Latitude = request.ArrivalLatitude,
                Longitude = request.ArrivalLongitude
            };
            var itemPlaceArrival = _mapper.Map<Place>(placeArrival);

            await _placeRepository.InsertAsync(itemPlaceArrival);
            await _unitOfWork.Commit(cancellationToken);

            var item = _mapper.Map<Carpool>(request);
            item.PlaceDepartureId = itemPlaceDeparture.Id;
            item.PlaceArrivalId = itemPlaceArrival.Id;
            item.UserName = id;

            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);

            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
