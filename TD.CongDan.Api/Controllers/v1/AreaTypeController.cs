using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Features.AreaTypes.Queries;

namespace TD.CongDan.Api.Controllers.v1
{
    public class AreaTypeController : BaseApiController<AreaTypeController>
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
{
            var items = await _mediator.Send(new GetAllAreaTypesQuery());
            return Ok(items);
        }

    }
}