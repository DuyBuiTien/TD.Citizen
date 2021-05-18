using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using TD.CongDan.Application.Features.JobSaveds.Commands;
using TD.CongDan.Application.Features.JobSaveds.Queries;

namespace TD.CongDan.Api.Controllers.v1
{
    public class JobSavedController : BaseApiController<JobSavedController>
    {
        //[AllowAnonymous]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            var items = await _mediator.Send(new GetAllJobSavedsQuery(pageNumber, pageSize, keySearch, orderBy));
            return Ok(items);
        }

        
        // POST api/<controller>
        [HttpPost]
        [Authorize]
       
        public async Task<IActionResult> Post(CreateJobSavedCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        //[Authorize(Policy = Permissions.Categories.Delete)]
        [Authorize]

        public async Task<IActionResult> Delete(int RecruitmentId)
        {
            return Ok(await _mediator.Send(new DeleteJobSavedCommand { RecruitmentId = RecruitmentId }));
        }
    }
}