using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers {
    [Route ("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase {
        private readonly ILoginService _loginService;
        public LoginController (ILoginService loginService) {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<object> Login ([FromBody] UserEntity user) {
            if (!ModelState.IsValid) return BadRequest (ModelState);

            try {
                return Ok (_loginService.FindByLogin (user));
            } catch (ArgumentException e) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}