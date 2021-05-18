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
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Features.Companies.Commands
{
    public partial class UpdateCompanyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InternationalName { get; set; }
        public string ShortName { get; set; }
        public string TaxCode { get; set; }
        //Dia chi cong ty
        //public string Address { get; set; }
        public string PlaceName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }

        //Dai dien
        public string Representative { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string ProfileVideo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        //Ngay cap
        public string DateOfIssueStr { get; set; }
        //Linh vuc kinh doanh
        public string Images { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        //Quy mo cong ty
        public string CompanySize { get; set; }

        public virtual ICollection<int> Industries { get; set; }


    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCompanyCommand, Result<int>>
    {
        private readonly ICompanyRepository _repository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly ICompanyIndustryRepository _companyIndustryRepository;


        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(ICompanyIndustryRepository companyIndustryRepository, ICompanyRepository repository, IPlaceRepository placeRepository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _placeRepository = placeRepository;
            _userManager = userManager;
            _authenticatedUser = authenticatedUser;
            _companyIndustryRepository = companyIndustryRepository;
        }

        public async Task<Result<int>> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
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

                DateTime? DateOfIssue = item.DateOfIssue;
                try { DateOfIssue = DateTime.ParseExact(command.DateOfIssueStr, "dd/MM/yyyy", provider); } catch { }

                var placeCount = _placeRepository.Places.Where(e => e.PlaceTypeId == 23 && e.ProvinceId == command.ProvinceId && e.DistrictId == command.DistrictId && e.CommuneId == command.CommuneId && e.PlaceName == command.PlaceName).Count();

                var placeId = item.PlaceId;


                if (placeCount < 1)
                {
                    Place place = new Place { PlaceName = command.PlaceName, ProvinceId = command.ProvinceId, DistrictId = command.DistrictId, CommuneId = command.CommuneId, PlaceTypeId = 23, Latitude = (double)command.Latitude, Longitude = (double)command.Longitude };
                    await _placeRepository.InsertAsync(place);
                    await _unitOfWork.Commit(cancellationToken);
                    placeId = place.Id;
                }

                item.Name = command.Name ?? item.Name;
                item.InternationalName = command.InternationalName ?? item.InternationalName;
                item.ShortName = command.ShortName ?? item.ShortName;
                item.TaxCode = command.TaxCode ?? item.TaxCode;
                item.Representative = command.Representative ?? item.Representative;
                item.PhoneNumber = command.PhoneNumber ?? item.PhoneNumber;
                item.Website = command.Website ?? item.Website;
                item.ProfileVideo = command.ProfileVideo ?? item.ProfileVideo;
                item.Fax = command.Fax ?? item.Fax;
                item.Images = command.Images ?? item.Images;
                item.Image = command.Image ?? item.Image;
                item.Logo = command.Logo ?? item.Logo;
                item.CompanySize = command.CompanySize ?? item.CompanySize;
                item.Description = command.Description ?? item.Description;
                item.DateOfIssue = DateOfIssue;
                item.PlaceId = placeId;

               // await _repository.UpdateAsync(item);


                var item_CompanyIndustries = item.CompanyIndustries;
                foreach (var item_ in item_CompanyIndustries)
                {
                    await _companyIndustryRepository.DeleteAsync(item_);
                }


               foreach (var _item in command.Industries)
                {
                    try
                    {
                        CompanyIndustry tmp = new CompanyIndustry { IndustryId = _item, CompanyId = item.Id };
                        await _companyIndustryRepository.InsertAsync(tmp);
                    }
                    catch
                    {

                    }
                }



                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}
