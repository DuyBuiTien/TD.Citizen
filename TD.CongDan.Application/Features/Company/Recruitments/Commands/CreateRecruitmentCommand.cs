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

namespace TD.CongDan.Application.Features.Recruitments.Commands
{
    public partial class CreateRecruitmentCommand : IRequest<Result<int>>
    {
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

         public string ResumeApplyExpired { get; set; }
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
        public virtual ICollection<RecruitmentBenefitCommand> ListBenefit { get; set; }
    }

    public class RecruitmentBenefitCommand
    {
        public string Name { get; set; }
        public int BenefitId { get; set; }

    }

    public class CreateCommandHandler : IRequestHandler<CreateRecruitmentCommand, Result<int>>
    {
        private readonly IRecruitmentRepository _repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IRecruitmentBenefitRepository _recruitmentBenefitRepository;

        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCommandHandler(IRecruitmentRepository repository, IPlaceRepository placeRepository, IRecruitmentBenefitRepository recruitmentBenefitRepository, ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _placeRepository = placeRepository;
            _companyRepository = companyRepository;
            _recruitmentBenefitRepository = recruitmentBenefitRepository;
        }

        public async Task<Result<int>> Handle(CreateRecruitmentCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.Username;

            CreatePlaceCommand place = new CreatePlaceCommand { PlaceName = request.PlaceName, ProvinceId = request.ProvinceId, DistrictId = request.DistrictId, CommuneId = request.CommuneId, PlaceTypeId = 23, Latitude = request.Latitude, Longitude = request.Longitude };
            var itemPlace = _mapper.Map<Place>(place);

            await _placeRepository.InsertAsync(itemPlace);
            await _unitOfWork.Commit(cancellationToken);

            var company = await _companyRepository.GetByUserNameAsync(id);
            Throw.Exception.IfNull(company, "Company", "No Company Found");

            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime? ResumeApplyExpired = null;
            try { ResumeApplyExpired = DateTime.ParseExact(request.ResumeApplyExpired, "dd/MM/yyyy", provider); } catch { }

            var item = _mapper.Map<Recruitment>(request);
            item.ResumeApplyExpired = ResumeApplyExpired;
            item.PlaceId = itemPlace.Id;
            item.UserName = id;

            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);

            if (request.ListBenefit != null)
                foreach (var _item in request.ListBenefit)
                {
                    try
                    {
                        RecruitmentBenefit tmp = new RecruitmentBenefit { BenefitId = _item.BenefitId, Name = _item.Name, RecruitmentId = item.Id };
                        await _recruitmentBenefitRepository.InsertAsync(tmp);
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
