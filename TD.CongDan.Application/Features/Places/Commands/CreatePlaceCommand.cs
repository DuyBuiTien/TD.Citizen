using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Application.Features.Places.Commands
{
    public partial class CreatePlaceCommand : IRequest<Result<int>>
    {
        public string PlaceName { get; set; }
        public string Title { get; set; }
        public string AddressDetail { get; set; }
        public string Source { get; set; }
        public string ExtraInfo { get; set; }
        public string PhoneContact { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string ContentHtml { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public string Tags { get; set; }
        public string Image { get; set; }
        public string Images { get; set; }
        public PlaceStatus Status { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }

        public int? PlaceTypeId { get; set; }
    }

    public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, Result<int>>
    {
        private readonly IPlaceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePlaceCommandHandler(IPlaceRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;
           
            var item = _mapper.Map<Place>(request);
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
