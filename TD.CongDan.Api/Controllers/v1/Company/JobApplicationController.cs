using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Features.JobNames.Queries;
using TD.CongDan.Application.Features.JobNames.Commands;
using TD.CongDan.Application.Features.JobApplications.Queries;
using TD.CongDan.Application.Features.JobApplications.Commands;

namespace TD.CongDan.Api.Controllers.v1
{
    public class JobApplicationController : BaseApiController<JobApplicationController>
    {
        /// <summary>
        /// Danh sách hồ sơ ứng tuyển của cá nhân - Cá nhân tự tạo hồ sơ để nhà tuyển dụng có thể theo dõi
        /// </summary>
        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string keySearch, string orderBy, string userName, int? currentPositionId, int? positionId, int? degreeId, int? experienceId, int? jobTypeId, int? isSearchAllowed)
        {
            var items = await _mediator.Send(new GetAllJobApplicationsQuery(pageNumber, pageSize, keySearch, orderBy, userName, currentPositionId, positionId, degreeId, experienceId, jobTypeId, isSearchAllowed));
            return Ok(items);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetJobApplicationByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        // [Authorize(Roles = "Admin")]
        //[Authorize(Policy = Permissions.Categories.Create)]

        public async Task<IActionResult> Post(CreateJobApplicationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateJobApplicationCommand command)
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
            return Ok(await _mediator.Send(new DeleteJobApplicationCommand { Id = id }));
        }
    }
}