namespace GatewayWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("/")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var value = Environment.GetEnvironmentVariable("APPID");
        return Ok($"Server {value}");
    }
}