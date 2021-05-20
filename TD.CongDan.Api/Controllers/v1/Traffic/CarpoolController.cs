using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using TD.CongDan.Application.Features.Carpools.Queries;
using TD.CongDan.Application.Features.Carpools.Commands;

namespace TD.CongDan.Api.Controllers.v1
{
    public class CarpoolController : BaseApiController<CarpoolController>
    {
        /// <summary>
        /// Danh sách đi chung xe
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string keySearch, string userName, string orderBy, decimal? price, decimal? priceTo, int? departureProvinceId, int? departureDistrictId, int? departureCommuneId, int? arrivalProvinceId, int? arrivalDistrictId, int? arrivalCommuneId, string departureDateStart, string departureDateEnd, int? status)
        {
            var items = await _mediator.Send(new GetAllCarpoolsQuery( pageNumber,  pageSize,  keySearch, userName, orderBy,  price, priceTo, departureProvinceId,  departureDistrictId, departureCommuneId, arrivalProvinceId,  arrivalDistrictId,  arrivalCommuneId,  departureDateStart,  departureDateEnd, status));
            return Ok(items);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetCarpoolByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        // [Authorize(Roles = "Admin")]
        //[Authorize(Policy = Permissions.Categories.Create)]

        public async Task<IActionResult> Post(CreateCarpoolCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateCarpoolCommand command)
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
            return Ok(await _mediator.Send(new DeleteCarpoolCommand { Id = id }));
        }
    }
}