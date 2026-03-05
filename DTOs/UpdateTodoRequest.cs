namespace TodoApi.DTOs;

public class UpdateTodoRequest
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}