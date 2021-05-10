using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Are.Commands
{
    public partial class CreateAreaCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string NameWithType { get; set; }
        public string Path { get; set; }
        public string PathWithType { get; set; }
        public string Description { get; set; }
    }

    public class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, Result<int>>
    {
        private readonly IAreaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IAreaTypeRepository _areaTypeRepository;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateAreaCommandHandler(IAreaRepository repository, IAreaTypeRepository areaTypeRepository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _areaTypeRepository = areaTypeRepository;
        }

        public async Task<Result<int>> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;


            if (!string.IsNullOrEmpty(request.Type))
            {
                var areaType = await _areaTypeRepository.GetByCodeAsync(request.Type);

                if (areaType != null)
                {
                    request.NameWithType = areaType.Name + " " + request.Name;
                }
            }
            if (!string.IsNullOrEmpty(request.ParentCode))
            {
                var areaParent = await _repository.GetByCodeAsync(request.ParentCode);
                if (areaParent!=null)
                {
                    var tmp_Path = !string.IsNullOrEmpty(areaParent.Path) ? areaParent.Path : areaParent.Name;
                    var tmp_PathWithType = !string.IsNullOrEmpty(areaParent.PathWithType) ? areaParent.PathWithType: areaParent.NameWithType;
                    request.Path = request.Name + ", " + tmp_Path;
                    request.PathWithType = request.NameWithType + ", " + tmp_PathWithType;
                }
            }
            
            
            var item = _mapper.Map<Area>(request);
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
