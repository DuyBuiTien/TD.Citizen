using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Constants;

using TD.CongDan.Application.Features.Places.Queries;
using TD.CongDan.Application.Features.Places.Commands;

namespace TD.CongDan.Api.Controllers.v1
{
    public class PlaceController : BaseApiController<PlaceController>
    {
        /// <summary>
        /// Danh sách địa điểm - có phân trang và lọc theo địa điểm gần tọa độ với khoảng khách range
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string parentCode, string type, string keySearch, string orderBy, double latitude, double longitude, double range, string placeTypeId)
{
            var items = await _mediator.Send(new GetAllPlaceQuery(pageNumber, pageSize, parentCode, type, keySearch, orderBy, latitude, longitude, range, placeTypeId));
            return Ok(items);
        }

        /* [HttpGet("fetchDuLieu")]
         public async Task<IActionResult> FetchDuLieu()
         {
             var items = await _mediator.Send(new FetchPlaceCommand());
             return Ok(items);
         }*/

        /*[HttpGet("fixDuLieu")]
        public async Task<IActionResult> FetchDuLieu()
        {
            var items = await _mediator.Send(new FetchPlaceCommand());
            return Ok(items);
        }*/

        /// <summary>Chi tiết địa điểm</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetPlaceByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Post(CreatePlaceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Put(int id, UpdatePlaceCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        //[Authorize(Policy = Permissions.Categories.Delete)]

        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePlaceCommand { Id = id }));
        }
    }
}