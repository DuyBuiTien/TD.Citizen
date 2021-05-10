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

namespace TD.CongDan.Application.Features.PlaceTypes.Commands
{
    public partial class UpdatePlaceTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdatePlaceTypeCommand, Result<int>>
    {
        private readonly IPlaceTypeRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(IPlaceTypeRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdatePlaceTypeCommand command, CancellationToken cancellationToken)
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
                item.Image = command.Image ?? item.Image;
                item.CoverImage = command.CoverImage ?? item.CoverImage;
                item.Description = command.Description ?? item.Description;
                item.CategoryId = command.CategoryId ?? item.CategoryId;


                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}

