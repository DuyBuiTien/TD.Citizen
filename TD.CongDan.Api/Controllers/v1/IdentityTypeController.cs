using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Features.IdentityTypes.Queries;

namespace TD.CongDan.Api.Controllers.v1
{
    public class IdentityTypeController : BaseApiController<IdentityTypeController>
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mediator.Send(new GetAllIdentityTypesQuery());
            return Ok(items);
        }

       
    }
}