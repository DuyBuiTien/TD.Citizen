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

namespace TD.CongDan.Application.Features.VehicleTypes.Commands
{
    public partial class UpdateVehicleTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public int? SeatCount { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateVehicleTypeCommand, Result<int>>
    {
        private readonly IVehicleTypeRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(IVehicleTypeRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateVehicleTypeCommand command, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null)
            {
                return Result<int>.Fail($"PlaceType Not Found.");
            }
            else
            {
                item.Name = command.Name ?? item.Name;
                item.Code = command.Code ?? item.Code;
                item.Icon = command.Icon ?? item.Icon;
                item.Description = command.Description ?? item.Description;
                item.SeatCount = command.SeatCount  ?? item.SeatCount;


                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}

