using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Features.Experiences.Queries;
using TD.CongDan.Application.Features.Experiences.Commands;

namespace TD.CongDan.Api.Controllers.v1
{
    public class ExperienceController : BaseApiController<ExperienceController>
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            var items = await _mediator.Send(new GetAllExperiencesQuery(pageNumber, pageSize, keySearch, orderBy));
            return Ok(items);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetExperienceByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        // [Authorize(Roles = "Admin")]
        //[Authorize(Policy = Permissions.Categories.Create)]

        public async Task<IActionResult> Post(CreateExperienceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Put(int id, UpdateExperienceCommand command)
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
        [Authorize(Roles = "Admin, SuperAdmin")]

        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteExperienceCommand { Id = id }));
        }
    }
}