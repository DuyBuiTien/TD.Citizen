using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using TD.CongDan.Application.Features.Salaries.Queries;
using TD.CongDan.Application.Features.Salaries.Commands;
using TD.CongDan.Application.Features.Recruitments.Commands;
using TD.CongDan.Application.Features.Recruitments.Queries;

namespace TD.CongDan.Api.Controllers.v1
{
    public class RecruitmentController : BaseApiController<RecruitmentController>
    {
        /// <summary>
        /// Danh sách việc làm
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string keySearch, string orderBy, int? companyId, string userName, int? provinceId, int? districtId, int? communeId, int? jobTypeId, int? jobNameId,
            int? jobPositionId, int? salaryId, int? experienceId, int? genderId, int? jobAgeId, int? degreeId, int? status, string resumeApplyExpiredStart, string resumeApplyExpiredEnd)
        {
            var items = await _mediator.Send(new GetAllRecruitmentsQuery(pageNumber, pageSize, keySearch, orderBy,  companyId,  userName,  provinceId,  districtId,  communeId, jobTypeId, jobNameId, jobPositionId, salaryId, experienceId, genderId, jobAgeId, degreeId, status, resumeApplyExpiredStart, resumeApplyExpiredEnd));
            return Ok(items);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetRecruitmentByIdQuery() { Id = id });
            return Ok(item);
        }


        [HttpPost("{id}/change-status")]
        [Authorize]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusRecruitmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }


        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateRecruitmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateRecruitmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteRecruitmentCommand { Id = id }));
        }
    }
}