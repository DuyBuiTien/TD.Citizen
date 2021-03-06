using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Features.JobTypes.Queries;
using TD.CongDan.Application.Features.JobTypes.Commands;
using TD.CongDan.Application.Features.LicensePlates.Queries;
using TD.CongDan.Application.Features.LicensePlates.Commands;

namespace TD.CongDan.Api.Controllers.v1
{
    public class LicensePlateController : BaseApiController<LicensePlateController>
    {
        /// <summary>
        /// Danh sách thông tin số xe của người dùng - phục vụ cho tra cứu phạt nguội
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string keySearch, string orderBy, string userName)
        {
            var items = await _mediator.Send(new GetAllLicensePlatesQuery(pageNumber, pageSize, keySearch, orderBy, userName));
            return Ok(items);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetLicensePlateByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        // [Authorize(Roles = "Admin")]
        //[Authorize(Policy = Permissions.Categories.Create)]

        public async Task<IActionResult> Post(CreateLicensePlateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateLicensePlateCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        //[Authorize(Policy = Permissions.Categories.Delete)]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteLicensePlateCommand { Id = id }));
        }
    }
}