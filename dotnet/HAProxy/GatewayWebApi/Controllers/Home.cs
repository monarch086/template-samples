namespace GatewayWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

//[Route("api/[controller]")]
[Route("/")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {

        return Ok("Server 01");
    }
}