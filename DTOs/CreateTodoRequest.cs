namespace TodoApi.DTOs;

public class CreateTodoRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
}