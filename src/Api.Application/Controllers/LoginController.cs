using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Domain.Dtos;
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
        public async Task<object> Login ([FromBody] LoginDTO user) {
            if (!ModelState.IsValid) return BadRequest (ModelState);
            try
            {
                var result = await _loginService.FindByLogin(user);
                if (result == null) return NotFound();
                return Ok (result);
            } catch (ArgumentException e) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}