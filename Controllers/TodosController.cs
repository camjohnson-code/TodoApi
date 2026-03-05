using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;
using TodoApi.Models;
using TodoApi.DTOs;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodoService _service;

    public TodosController(TodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll(1));
    }

    [HttpGet("{id}")]
    public IActionResult GetTodo(int id)
    {
        TodoItem? todo = _service.GetById(id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    public IActionResult CreateTodo([FromBody] CreateTodoRequest request)
    {
        return Ok(_service.Create(request.Title, request.Description, 1));
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateTodo(int id, [FromBody] UpdateTodoRequest request)
    {
        TodoItem? result = _service.Update(id, request.Title, request.Description);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(int id)
    {
        if (_service.Delete(id) == true) return NoContent();
        else return NotFound();
    }

    [HttpPatch("{id}/toggle")]
    public IActionResult ToggleCompletion(int id)
    {
        TodoItem? result = _service.ToggleCompletion(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}