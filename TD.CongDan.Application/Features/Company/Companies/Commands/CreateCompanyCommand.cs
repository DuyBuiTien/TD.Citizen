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

namespace TD.CongDan.Application.Features.Companies.Commands
{
    public partial class CreateCompanyCommand : IRequest<Result<int>>
    {
        public string UserId { get; set; }
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
        //Ngay cap
        public string DateOfIssueStr { get; set; }
        //Linh vuc kinh doanh
        public string BusinessSector { get; set; }
        public string Images { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        //Quy mo cong ty
        public string CompanySize { get; set; }
    }

    public class CreateCommandHandler : IRequestHandler<CreateCompanyCommand, Result<int>>
    {
        private readonly ICompanyRepository _repository;
        private readonly IPlaceRepository _placeRepository;

        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCommandHandler(ICompanyRepository repository, IPlaceRepository placeRepository,  IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _placeRepository = placeRepository;
        }

        public async Task<Result<int>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.Username;
            request.UserId = id;

            Place place = new Place { PlaceName = request.PlaceName, ProvinceId = request.ProvinceId, DistrictId = request.DistrictId, CommuneId = request.CommuneId, PlaceTypeId=23, Latitude = (double)request.Latitude, Longitude = (double)request.Longitude };
            await _placeRepository.InsertAsync(place);
            await _unitOfWork.Commit(cancellationToken);

            

            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime? DateOfIssue = null;
            try { DateOfIssue = DateTime.ParseExact(request.DateOfIssueStr, "dd/MM/yyyy", provider); } catch { }

            var item = _mapper.Map<Company>(request);
            item.DateOfIssue = DateOfIssue;
            item.PlaceId = place.Id;

            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
