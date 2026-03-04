namespace TodoApi.Data;

using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TodoItem> Todos { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}