using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Constants;
using TD.CongDan.Application.Features.PlaceTypes.Queries;
using TD.CongDan.Application.Features.PlaceTypes.Commands;

namespace TD.CongDan.Api.Controllers.v1
{
    public class PlaceTypeController : BaseApiController<PlaceTypeController>
    {
        /// <summary>
        /// Danh sách loại địa điểm - Có phân trang và lọc
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string parentCode, string type, string keySearch, string orderBy, int categoryId)
{
            var items = await _mediator.Send(new GetAllPlaceTypeQuery(pageNumber, pageSize, parentCode, type, keySearch, orderBy, categoryId));
            return Ok(items);
        }

        /*/// <summary>
        /// Chi tiết loại địa điểm
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetPlaceByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        // [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin, SuperAdmin")]

        public async Task<IActionResult> Post(CreatePlaceTypeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Put(int id, UpdatePlaceTypeCommand command)
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
            return Ok(await _mediator.Send(new DeletePlaceTypeCommand { Id = id }));
        }*/
    }
}