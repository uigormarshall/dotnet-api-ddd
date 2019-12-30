using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers {
    [ApiController]
    [Route ("api/v1/users")]
    public class UsersController : ControllerBase {
        [HttpGet]
        public async Task<ActionResult> GetAll ([FromServices] IUserService service )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(await service.GetAll());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}