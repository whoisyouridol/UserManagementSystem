using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.BAL;
using UserManagementSystem.BAL.DTOs.Request;
using UserManagementSystem.BAL.DTOs.Response;

namespace UserManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBusinessAccessLayerService _service;

        public UserController(IBusinessAccessLayerService businessAccessLayerService)
        {
            _service = businessAccessLayerService;
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Add(AddUserRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = await _service.Add(request);

            return Ok(id);
        }

        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _service.Update(request);
            return NoContent();
        }

        [ProducesResponseType(200,Type = typeof(GetUserResponseDTO))]
        [ProducesResponseType(400)]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var user = await _service.Get(id);
            return Ok(user);
        }

        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            await _service.Delete(id);
            return NoContent();
        }
    }
}
