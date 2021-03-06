using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Features.Brands.Commands.Create
{
    public partial class CreateBrandCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
    }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<int>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Brand>(request);
            await _brandRepository.InsertAsync(product);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(product.Id);
        }
    }
}