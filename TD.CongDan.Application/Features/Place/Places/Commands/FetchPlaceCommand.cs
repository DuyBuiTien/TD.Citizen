using TD.Libs.Results;
using AutoMapper;
using MediatR;

using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Places.Commands
{
    public partial class FetchPlaceCommand : IRequest<Result<string>>
    {
       
    }

    public class FetchPlaceCommandHandler : IRequestHandler<FetchPlaceCommand, Result<string>>
    {
        private readonly IPlaceTypeRepository _placeTypeRepository;
        private readonly IPlaceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAreaRepository _iAreaRepository;

        private IUnitOfWork _unitOfWork { get; set; }

        public FetchPlaceCommandHandler(IPlaceTypeRepository placeTypeRepository, IPlaceRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAreaRepository iAreaRepository)
        {
            _placeTypeRepository = placeTypeRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iAreaRepository = iAreaRepository;
        }

        public async Task<Result<string>> Handle(FetchPlaceCommand command, CancellationToken cancellationToken)
        {
            var offset = 0;



            var placeList = await _repository.GetListAsync();


           foreach (Place item in placeList)
            {
               // item.Location = geometryFactory.CreatePoint(new Coordinate(Decimal.ToDouble(item.Latitude), Decimal.ToDouble(item.Longitude)));
                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
            }


            var count = placeList.Count;

          

            return  Result<string>.Success("Done!!!" + count);
        }
    }
}

