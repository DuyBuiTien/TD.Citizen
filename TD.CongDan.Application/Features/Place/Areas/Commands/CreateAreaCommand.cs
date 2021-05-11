using TD.Libs.Results;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;
using RestSharp;

namespace TD.CongDan.Application.Features.Are.Commands
{
    public partial class CreateAreaCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
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

        [System.Obsolete]
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
                if (areaParent != null)
                {
                    var tmp_Path = !string.IsNullOrEmpty(areaParent.Path) ? areaParent.Path : areaParent.Name;
                    var tmp_PathWithType = !string.IsNullOrEmpty(areaParent.PathWithType) ? areaParent.PathWithType : areaParent.NameWithType;
                    request.Path = request.Name + ", " + tmp_Path;
                    request.PathWithType = request.NameWithType + ", " + tmp_PathWithType;
                }
            }

            var item = _mapper.Map<Area>(request);
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);

            /*var client = new RestClient();
            var request_ = new RestRequest("https://raw.githubusercontent.com/daohoangson/dvhcvn/master/data/dvhcvn.json");
            var cancellationTokenSource = new CancellationTokenSource();

            var restResponse =
                await client.ExecuteTaskAsync(request_, cancellationTokenSource.Token);

            var content = restResponse.Content;
            Root account = JsonConvert.DeserializeObject<Root>(content);


            foreach (var item in account.data)
            {
               
                var tinh = new Area
                {
                    Code = item.level1_id,
                    Name = item.name,
                    NameWithType = item.name,
                    Level = 1,
                    Type = item.type,
                    Slug = convert(item.name),
                    Path = item.name,
                    PathWithType = item.name,
                };
                //var tinh_ = _mapper.Map<Area>(tinh);
                await _repository.InsertAsync(tinh);
                
                foreach (var itemLv2 in item.level2s)
                {
                    var huyen = new Area
                    {
                        Code = itemLv2.level2_id,
                        Name = itemLv2.name,
                        NameWithType = itemLv2.name,
                        Level = 2,
                        Type = itemLv2.type,
                        Slug = convert(itemLv2.name),
                        ParentCode = item.level1_id,
                        Path = itemLv2.name + ", " + tinh.Path,
                        PathWithType = itemLv2.name + ", " + tinh.PathWithType,
                    };
                    //var huyen_ = _mapper.Map<Area>(huyen);
                    await _repository.InsertAsync(huyen);
                    
                    foreach (var itemLv3 in itemLv2.level3s)
                    {
                        var xa = new Area
                        {
                            Code = itemLv3.level3_id,
                            Name = itemLv3.name,
                            NameWithType = itemLv3.name,
                            Level = 3,
                            Type = itemLv3.type,
                            Slug = convert(itemLv3.name),
                            ParentCode = itemLv2.level2_id,
                            Path = itemLv3.name + ", " + huyen.Path,
                            PathWithType = itemLv3.name + ", " + huyen.PathWithType,
                        };
                       // var xa_ = _mapper.Map<Area>(xa);
                        await _repository.InsertAsync(xa);

                    }
                }
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(0);*/
        }
        public static string convert(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }



    public class Level3s
    {
        public string level3_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Level2s
    {
        public string level2_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<Level3s> level3s { get; set; }
    }

    public class Datum
    {
        public string level1_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<Level2s> level2s { get; set; }
    }

    public class Stats
    {
        public double elapsed_time { get; set; }
        public int level1_count { get; set; }
        public int level2_count { get; set; }
        public int level3_count { get; set; }
    }

    public class Root
    {
        public List<Datum> data { get; set; }
        public string data_date { get; set; }
        public int generate_date { get; set; }
        public Stats stats { get; set; }
    }

}
