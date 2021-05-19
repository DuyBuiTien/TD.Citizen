using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TD.CongDan.Application.Features.MaritalStatuses.Queries;

namespace TD.CongDan.Api.Controllers.v1
{
    public class MaritalStatusController : BaseApiController<MaritalStatusController>
    {
        /// <summary>
        /// Danh sách tình trạng hôn nhân
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mediator.Send(new GetAllMaritalStatusesQuery());
            return Ok(items);
        }


    }
}