using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using UserManagementSystem.BAL;

namespace UserManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalDataController : ControllerBase
    {
        private readonly IBusinessAccessLayerService _service;

        public ExternalDataController(IBusinessAccessLayerService service)
        {
            _service = service;
        }

        [HttpGet("posts")]
        public async Task<IActionResult> Posts(int userId)
        {
            var result = await _service.GetPosts(userId);
            return Ok(result);
        }
        [HttpGet("albums")]
        public async Task<IActionResult> Albums(int userId)
        {
            var result = await _service.GetAlbums(userId);
            return Ok(result);
        }
        [HttpGet("todos")]
        public async Task<IActionResult> Todos(int userId)
        {
            var result = await _service.GetTodos(userId);
            return Ok(result);
        }
    }
}
