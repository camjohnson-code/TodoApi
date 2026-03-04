namespace TodoApi.Models;

public class TodoItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}