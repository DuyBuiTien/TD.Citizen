using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Features.Genders.Queries;

namespace TD.CongDan.Api.Controllers.v1
{
    public class GenderController : BaseApiController<GenderController>
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mediator.Send(new GetAllGendersQuery());
            return Ok(items);
        }

       
    }
}