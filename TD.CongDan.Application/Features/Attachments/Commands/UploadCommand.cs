using TD.Libs.Results;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Attachments.Commands
{
    public partial class UploadCommand : IRequest<Result<List<Attachment>>>
    {
        public List<IFormFile> Files { get; set; }

    }

    public class UploadCommandHandler : IRequestHandler<UploadCommand, Result<List<Attachment>>>
    {
        private readonly IAttachmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public UploadCommandHandler(IAttachmentRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<List<Attachment>>> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;
            var files = request.Files;

            long size = files.Sum(f => f.Length);

            if (files.Any(f => f.Length == 0))
            {
                return Result<List<Attachment>>.Fail("Loi upload file");
            }
            var folderName = Path.Combine("Resources", "Files");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            int count = 0;
            List<Attachment> listFile = new List<Attachment>();


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = formFile.FileName;

                    Guid dir_UUID = Guid.NewGuid();
                    string dir_UUID_String = dir_UUID.ToString();


                    var target = Path.Combine(pathToSave, dir_UUID_String);
                    if (!Directory.Exists(target))
                    {
                        Directory.CreateDirectory(target);
                    }

                    var fullPath = Path.Combine(target, fileName);
                    var dbPath = Path.Combine(folderName, dir_UUID_String, fileName);

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                        var attachment = new Attachment();
                        attachment.Name = fileName;
                        attachment.Type = Path.GetExtension(formFile.FileName);
                        attachment.Url = dbPath.Replace("\\", "/");
                        var tmp = await _repository.InsertAsync(attachment);
                        listFile.Add(tmp);
                        count++;
                    }
                }
            }


            await _unitOfWork.Commit(cancellationToken);
            return Result<List<Attachment>>.Success(listFile);
        }
    }
}
