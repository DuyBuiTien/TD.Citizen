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

namespace TD.CongDan.Application.Features.TrafficTickets.Commands
{
    public partial class UpdateTrafficTicketCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
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
        public int? Price { get; set; }
        //Ảnh vi phạm
        public string Images { get; set; }
        //Trạng thái - 0: Chưa xử lý, 1 = : đang xử lý, 2 = đã xử lý
        public int? Status { get; set; }
        //Mô tả thêm
        public string Description { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateTrafficTicketCommand, Result<int>>
    {
        private readonly ITrafficTicketRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(ITrafficTicketRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateTrafficTicketCommand command, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null)
            {
                return Result<int>.Fail($"PlaceType Not Found.");
            }
            else
            {
                item.LicensePlateNumber = command.LicensePlateNumber ?? item.LicensePlateNumber;
                item.Behaviour = command.Behaviour ?? item.Behaviour;
                item.Location = command.Location ?? item.Location;
                item.DateOfOffence = command.DateOfOffence ?? item.DateOfOffence;
                item.Device = command.Device ?? item.Device;
                item.Description = command.Description ?? item.Description;
                item.PhoneNumber = command.PhoneNumber ?? item.PhoneNumber;
                item.Unit = command.Unit ?? item.Unit;
                item.Price = command.Price ?? item.Price;
                item.Images = command.Images ?? item.Images;
                item.Status = command.Status ?? item.Status;


                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}

