using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TD.CongDan.Application.Features.Categories.Queries.GetById;
using TD.CongDan.Application.Features.Categories.Commands;
using TD.CongDan.Application.Features.Categories.Queries.GetAllPaged;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Constants;

namespace TD.CongDan.Api.Controllers.v1
{
    public class CategoryController : BaseApiController<CategoryController>
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var items = await _mediator.Send(new GetAllCategoriesQuery(pageNumber, pageSize));
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        // [Authorize(Roles = "Admin")]
        //[Authorize(Policy = Permissions.Categories.Create)]

        public async Task<IActionResult> Post(CreateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCategoryCommand command)
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
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}