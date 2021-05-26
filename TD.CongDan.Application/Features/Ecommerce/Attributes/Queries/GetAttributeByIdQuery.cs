using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Attributes.Queries
{
    public class GetAttributeByIdQuery : IRequest<Result<AttributeResponse>>
    {
        public int Id { get; set; }

        public class GetAreaByIdQueryHandler : IRequestHandler<GetAttributeByIdQuery, Result<AttributeResponse>>
        {
            private readonly IAttributeRepository _repository;
            private readonly IMapper _mapper;

            public GetAreaByIdQueryHandler(IAttributeRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<AttributeResponse>> Handle(GetAttributeByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mapped = _mapper.Map<AttributeResponse>(item);
                return Result<AttributeResponse>.Success(mapped);
            }
        }
    }
}