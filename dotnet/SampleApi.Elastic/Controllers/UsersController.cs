using Microsoft.AspNetCore.Mvc;
using SampleApi.Elastic.Models;
using SampleApi.Elastic.Services;

namespace SampleApi.Elastic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IElasticService _elasticService;

        public UsersController(ILogger<UsersController> logger, IElasticService elasticService)
        {
            _logger = logger;
            _elasticService = elasticService;
        }

        [HttpPost("create-index")]
        public async Task<IActionResult> CreateIndex([FromQuery] string indexName)
        {
            await _elasticService.CreateIndexIfNotExistsAsync(indexName);
            return Ok($"Index {indexName} created or already exists.");
        }

        [HttpPost("users")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var result = await _elasticService.AddOrUpdate(user);
            return result ? Ok($"User added successfully.")
                : StatusCode(500, "Error adding or updating user.");
        }

        [HttpPut("users")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var result = await _elasticService.AddOrUpdate(user);
            return result ? Ok($"User updated successfully.")
                : StatusCode(500, "Error updating user.");
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUser(string key)
        {
            var result = await _elasticService.Get(key);
            return result != null ? Ok(result)
                : NotFound("User not found.");
        }

        [HttpDelete("users")]
        public async Task<IActionResult> DeleteUser(string key)
        {
            var result = await _elasticService.Remove(key);
            return result ? Ok($"User deleted successfully.")
                : StatusCode(500, "Error deleting user.");
        }
    }
}
