using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
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
        
        [HttpGet]
        [Route ("{id}")]
        public async Task<ActionResult> Get (Guid id, [FromServices] IUserService service )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(await service.Get(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Post (UserEntity user, [FromServices] IUserService service )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(await service.Post(user));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update (UserEntity user, [FromServices] IUserService service )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await service.Put(user);
                if(result == null) return new BadRequestResult(); 
                return Ok(await service.Put(user));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpDelete]
        [Route ("{id}")]
        public async Task<ActionResult> Delete (Guid id, [FromServices] IUserService service )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(await service.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}