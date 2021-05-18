using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Features.Degrees.Queries;
using TD.CongDan.Application.Features.Degrees.Commands;
using TD.CongDan.Application.Features.Benefits.Queries;
using TD.CongDan.Application.Features.Benefits.Commands;
using TD.CongDan.Application.Features.Companies.Queries;
using TD.CongDan.Application.Features.Companies.Commands;

namespace TD.CongDan.Api.Controllers.v1
{
    public class CompanyController : BaseApiController<CompanyController>
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string keySearch, string orderBy, string userId, int? provinceId, int? districtId, int? communeId)
        {
            var items = await _mediator.Send(new GetAllCompaniesQuery(pageNumber, pageSize, keySearch, orderBy, userId, provinceId, districtId, communeId));
            return Ok(items);
        }


        [Authorize]
        [HttpGet("current-company")]
        public async Task<IActionResult> GetCurrentCompany()
        {
            var item = await _mediator.Send(new GetCurrentCompanyQuery());
            return Ok(item);
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetCompanyByIdQuery() { Id = id });
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        // [Authorize(Roles = "Admin")]
        //[Authorize(Policy = Permissions.Categories.Create)]

        public async Task<IActionResult> Post(CreateCompanyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateCompanyCommand command)
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
            return Ok(await _mediator.Send(new DeleteCompanyCommand { Id = id }));
        }
    }
}