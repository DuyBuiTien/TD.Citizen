using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Constants;
using TD.CongDan.Application.Features.Are.Queries;
using TD.CongDan.Application.Features.Are.Commands;


namespace TD.CongDan.Api.Controllers.v1
{
    public class AreaController : BaseApiController<AreaController>
    {
        /// <summary>
        /// Danh sách địa bàn có phân trang, tìm kiếm, lọc theo level= 1: tỉnh, thành phố trực thuộc trung ương, level = 2 : quận, huyện, level =3: phường xã; Lọc theo ParentId: Địa bàn cấp cha
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string parentCode, string type, string keySearch, string orderBy, int? level, int? parentId)
{
            var items = await _mediator.Send(new GetAllAreaQuery(pageNumber, pageSize, parentCode, type, keySearch, orderBy, level, parentId));
            return Ok(items);
        }

        /// <summary>
        /// Chi tiết địa bàn
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetAreaByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        /// <summary>
        /// Thêm mới địa bàn
        /// </summary>
        /*[HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Post(CreateAreaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }*/

        // PUT api/<controller>/5
        //[HttpPut("{id}")]
       /* [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Put(int id, UpdateAreaCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }*/

        // DELETE api/<controller>/5
        /*[HttpDelete("{id}")]
        *//*[Authorize(Policy = Permissions.Categories.Delete)]*//*
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteAreaCommand { Id = id }));
        }*/
    }
}