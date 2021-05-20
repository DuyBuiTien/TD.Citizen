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

namespace TD.CongDan.Application.Features.TrafficTickets.Commands
{
    public partial class CreateTrafficTicketCommand : IRequest<Result<int>>
    {
        public string LicensePlateNumber { get; set; }
        //hành vi vi phạm
        public string Behaviour { get; set; }
        //Địa điểm
        public string Location { get; set; }
        public DateTime? DateOfOffence { get; set; }
        //Thiết bị phát hiện
        public string Device { get; set; }
        //Đơn vị phát hiện
        public string Unit { get; set; }
        //Số điện thoại liên hệ
        public string PhoneNumber { get; set; }
        //Tiền phạt
        public int Price { get; set; }
        //Ảnh vi phạm
        public string Images { get; set; }
        //Trạng thái - 0: Chưa xử lý, 1 = : đang xử lý, 2 = đã xử lý
        public int Status { get; set; }
        //Mô tả thêm
        public string Description { get; set; }
    }

    public class CreatePlaceTypeCommandHandler : IRequestHandler<CreateTrafficTicketCommand, Result<int>>
    {
        private readonly ITrafficTicketRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePlaceTypeCommandHandler(ITrafficTicketRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreateTrafficTicketCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;
           
            var item = _mapper.Map<TrafficTicket>(request);
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
