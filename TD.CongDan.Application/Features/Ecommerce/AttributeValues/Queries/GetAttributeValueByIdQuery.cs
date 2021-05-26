using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.AttributeValues.Queries
{
    public class GetAttributeValueByIdQuery : IRequest<Result<AttributeValueResponse>>
    {
        public int Id { get; set; }

        public class GetAreaByIdQueryHandler : IRequestHandler<GetAttributeValueByIdQuery, Result<AttributeValueResponse>>
        {
            private readonly IAttributeValueRepository _repository;
            private readonly IMapper _mapper;

            public GetAreaByIdQueryHandler(IAttributeValueRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<AttributeValueResponse>> Handle(GetAttributeValueByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mapped = _mapper.Map<AttributeValueResponse>(item);
                return Result<AttributeValueResponse>.Success(mapped);
            }
        }
    }
}