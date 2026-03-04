using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("Success");
    }
}