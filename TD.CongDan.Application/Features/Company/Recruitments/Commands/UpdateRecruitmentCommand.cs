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

namespace TD.CongDan.Application.Features.Recruitments.Commands
{
    public partial class UpdateRecruitmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        //Loai hinh cong viec
        public int? JobTypeId { get; set; }
        //Nghe nghiep
        public int? JobNameId { get; set; }
        //Vi tri
        public int? JobPositionId { get; set; }
        //Muc luong
        public int? SalaryId { get; set; }

        //Kinh nghiem
        public int? ExperienceId { get; set; }
        //Yeu cau gioi tinh
        public int? GenderId { get; set; }
        public int? JobAgeId { get; set; }
        public int? DegreeId { get; set; }

        //yeu cau khac
        public string OtherRequirement { get; set; }
        //ho so bao gom
        public string ResumeRequirement { get; set; }

        public DateTime? ResumeApplyExpired { get; set; }
        //So luong
        public int NumberOfJob { get; set; }

        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactAdress { get; set; }

        public string PlaceName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }
    }



    public class UpdateCommandHandler : IRequestHandler<UpdateRecruitmentCommand, Result<int>>
    {
        private readonly IRecruitmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IRecruitmentBenefitRepository _recruitmentBenefitRepository;
        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(IRecruitmentRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser, UserManager<ApplicationUser> userManager, IPlaceRepository placeRepository, IRecruitmentBenefitRepository recruitmentBenefitRepository, ICompanyRepository companyRepository)
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

        public async Task<Result<int>> Handle(UpdateRecruitmentCommand command, CancellationToken cancellationToken)
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

                CultureInfo provider = CultureInfo.InvariantCulture;

                /* DateTime? ResumeApplyExpired = item.ResumeApplyExpired;
                 try { ResumeApplyExpired = DateTime.ParseExact(command.ResumeApplyExpired, "dd/MM/yyyy", provider); } catch { }*/

                /* var placeCount = _placeRepository.Places.Where(e => e.PlaceTypeId == 23 && e.ProvinceId == command.ProvinceId && e.DistrictId == command.DistrictId && e.CommuneId == command.CommuneId && e.PlaceName == command.PlaceName).Count();

                 var placeId = item.PlaceId;

                 if (placeCount < 1)
                 {
                     Place place = new Place { PlaceName = command.PlaceName, ProvinceId = command.ProvinceId, DistrictId = command.DistrictId, CommuneId = command.CommuneId, PlaceTypeId = 23, Latitude = (double)command.Latitude, Longitude = (double)command.Longitude };
                     await _placeRepository.InsertAsync(place);
                     await _unitOfWork.Commit(cancellationToken);
                     placeId = place.Id;
                 }*/

                var place = item.Place;
                place.PlaceName = command.PlaceName ?? place.PlaceName;
                place.ProvinceId = command.ProvinceId ?? place.ProvinceId;
                place.DistrictId = command.DistrictId ?? place.DistrictId;
                place.Latitude = command.Latitude ?? place.Latitude;
                place.Longitude = command.Longitude ?? place.Longitude;


                item.Name = command.Name ?? item.Name;
                item.Description = command.Description ?? item.Description;
                item.Image = command.Image ?? item.Image;
                item.JobTypeId = command.JobTypeId ?? item.JobTypeId;
                item.JobNameId = command.JobNameId ?? item.JobNameId;
                item.JobPositionId = command.JobPositionId ?? item.JobPositionId;
                item.SalaryId = command.SalaryId ?? item.SalaryId;
                item.ExperienceId = command.ExperienceId ?? item.ExperienceId;
                item.GenderId = command.GenderId ?? item.GenderId;
                item.JobAgeId = command.JobAgeId ?? item.JobAgeId;
                item.DegreeId = command.DegreeId ?? item.DegreeId;
                item.OtherRequirement = command.OtherRequirement ?? item.OtherRequirement;
                item.ResumeRequirement = command.ResumeRequirement ?? item.ResumeRequirement;
                item.ResumeApplyExpired = command.ResumeApplyExpired ?? item.ResumeApplyExpired;
                item.NumberOfJob = command.NumberOfJob>=0 ? command.NumberOfJob : item.NumberOfJob;
                item.ContactName = command.ContactName ?? item.ContactName;
                item.ContactEmail = command.ContactEmail ?? item.ContactEmail;
                item.ContactPhone = command.ContactPhone ?? item.ContactPhone;
                item.ContactAdress = command.ContactAdress ?? item.ContactAdress;


                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
