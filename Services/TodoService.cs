namespace TodoApi.Services;

using TodoApi.Models;
using TodoApi.Data;

public class TodoService
{
    private readonly AppDbContext _context;

    public TodoService(AppDbContext context)
    {
        _context = context;
    }

    public List<TodoItem> GetAll(int userId)
    {
        return
        _context.Todos.Where(t => t.UserId == userId).ToList();
    }
    public TodoItem? GetById(int todoId)
    {
        return _context.Todos.FirstOrDefault(t => t.Id == todoId);
    }

    public TodoItem Create(string title, string? description, int userId)
    {
        TodoItem newTodo = new TodoItem { Title = title, Description = description, IsCompleted = false, CreatedAt = DateTime.UtcNow, UserId = userId };
        _context.Todos.Add(newTodo);
        _context.SaveChanges();
        return newTodo;
    }

    public TodoItem? Update(int todoId, string? title, string? description)
    {
        TodoItem? existingTodo = _context.Todos.FirstOrDefault(t => t.Id == todoId);
        if (existingTodo == null) return null;
        if (title != null) existingTodo.Title = title;
        if (description != null) existingTodo.Description = description;
        _context.SaveChanges();
        return existingTodo;
    }
    
    public bool Delete(int todoId)
    {
        TodoItem? existingTodo = _context.Todos.FirstOrDefault(t => t.Id == todoId);
        if (existingTodo == null) return false;
        _context.Todos.Remove(existingTodo);
        _context.SaveChanges();
        return true;
    }

    public TodoItem? ToggleCompletion(int todoId)
    {
        TodoItem? existingTodo = _context.Todos.FirstOrDefault(t => t.Id == todoId);
        if (existingTodo == null) return null;
        existingTodo.IsCompleted = !existingTodo.IsCompleted;
        _context.SaveChanges();
        return existingTodo;
    }
}