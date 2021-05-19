using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TD.CongDan.Application.Features.Categories.Queries.GetById;
using TD.CongDan.Application.Features.Categories.Commands;
using TD.CongDan.Application.Features.Categories.Queries.GetAllPaged;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Constants;
using System.IO;
using TD.CongDan.Application.Features.Attachments.Commands;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

namespace TD.CongDan.Api.Controllers.v1
{
    public class FileController : BaseApiController<CategoryController>
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });
            return Ok(item);
        }


        /// <summary>
        /// Upload file đính kèm lên server, kết quả trả về đường dẫn file
        /// </summary>
        // POST api/<controller>
        [HttpPost, DisableRequestSizeLimit]
        [Authorize]

        public async Task<IActionResult> Post([FromForm(Name = "files")] List<IFormFile> files)
        {
            return Ok(await _mediator.Send(new UploadCommand() { Files = files }));
        }
    }
}