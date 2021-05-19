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
using TD.CongDan.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Application.Interfaces.Shared;
using System.Globalization;
using TD.CongDan.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace TD.CongDan.Application.Features.Carpools.Commands
{
    public partial class UpdateCarpoolCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
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
        public decimal? Price { get; set; }
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

        public int? Status { get; set; }
    }



    public class UpdateCommandHandler : IRequestHandler<UpdateCarpoolCommand, Result<int>>
    {
        private readonly ICarpoolRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IRecruitmentBenefitRepository _recruitmentBenefitRepository;
        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(ICarpoolRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser, UserManager<ApplicationUser> userManager, IPlaceRepository placeRepository, IRecruitmentBenefitRepository recruitmentBenefitRepository, ICompanyRepository companyRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _authenticatedUser = authenticatedUser;
            _placeRepository = placeRepository;
            _companyRepository = companyRepository;
            _recruitmentBenefitRepository = recruitmentBenefitRepository;
        }

        public async Task<Result<int>> Handle(UpdateCarpoolCommand command, CancellationToken cancellationToken)
        {
            var userName = _authenticatedUser.Username;
            var user = await _userManager.FindByNameAsync(userName);
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null || !(rolesList.Contains("Admin") || rolesList.Contains("SuperAdmin") || userName == item.UserName))
            {
                return Result<int>.Fail($"Lỗi!");
            }
            else
            {
                var placeArrival = item.PlaceArrival;
                placeArrival.PlaceName = command.ArrivalPlaceName ?? placeArrival.PlaceName;
                placeArrival.ProvinceId = command.ArrivalProvinceId ?? placeArrival.ProvinceId;
                placeArrival.DistrictId = command.ArrivalDistrictId ?? placeArrival.DistrictId;
                placeArrival.Latitude = command.ArrivalLatitude ?? placeArrival.Latitude;
                placeArrival.Longitude = command.ArrivalLongitude ?? placeArrival.Longitude;

                var placeDeparture = item.PlaceDeparture;
                placeDeparture.PlaceName = command.ArrivalPlaceName ?? placeDeparture.PlaceName;
                placeDeparture.ProvinceId = command.ArrivalProvinceId ?? placeDeparture.ProvinceId;
                placeDeparture.DistrictId = command.ArrivalDistrictId ?? placeDeparture.DistrictId;
                placeDeparture.Latitude = command.ArrivalLatitude ?? placeDeparture.Latitude;
                placeDeparture.Longitude = command.ArrivalLongitude ?? placeDeparture.Longitude;



                item.Name = command.Name ?? item.Name;
                item.Description = command.Description ?? item.Description;
                item.PhoneNumber = command.PhoneNumber ?? item.PhoneNumber;
                item.DepartureDate = command.DepartureDate ?? item.DepartureDate;
                item.DepartureTime = command.DepartureTime ?? item.DepartureTime;
                item.VehicleTypeId = command.VehicleTypeId ?? item.VehicleTypeId;
                item.Role = command.Role ?? item.Role;
                item.Price = command.Price ?? item.Price;
                item.SeatCount = command.SeatCount ?? item.SeatCount;
                item.Status = command.Status ?? item.Status;

                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
