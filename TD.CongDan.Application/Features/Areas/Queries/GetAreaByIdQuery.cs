using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Are.Queries
{
    public class GetAreaByIdQuery : IRequest<Result<AreaResponse>>
    {
        public int Id { get; set; }

        public class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, Result<AreaResponse>>
        {
            private readonly IAreaRepository _areaRepository;
            private readonly IAreaTypeRepository _areaTypeRepository;
            private readonly IMapper _mapper;

            public GetAreaByIdQueryHandler(IAreaRepository areaRepository, IAreaTypeRepository areaTypeRepository, IMapper mapper)
            {
                _areaRepository = areaRepository;
                _areaTypeRepository = areaTypeRepository;
                _mapper = mapper;
            }

            public async Task<Result<AreaResponse>> Handle(GetAreaByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _areaRepository.GetByIdAsync(query.Id);
                //var mappedCategory = _mapper.Map<AreasResponse>(category);

                //var areaType = await _areaTypeRepository.GetByCodeAsync(item.Type);
                Area areaParent = null;
                if (!string.IsNullOrEmpty(item.ParentCode))
                {
                    areaParent = await _areaRepository.GetByCodeAsync(item.ParentCode); 
                }
                


                var categoryRes = new AreaResponse();
                categoryRes.Id = item.Id;
                categoryRes.Code = item.Code;
                categoryRes.Name = item.Name;
                categoryRes.ParentCode = item.ParentCode;
                categoryRes.NameWithType = item.NameWithType;
                categoryRes.Path = item.Path;
                categoryRes.PathWithType = item.PathWithType;
                categoryRes.Slug = item.Slug;
                categoryRes.Type = item.Type;
                //categoryRes.AreaType = areaType;
                categoryRes.ParentArea = areaParent;
                    

                return Result<AreaResponse>.Success(categoryRes);
            }
        }
    }
}