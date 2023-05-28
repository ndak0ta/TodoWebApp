using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;

namespace Todo.Data.Contexts;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }
    
    public DbSet<TodoItem>? TodoItem { get; set; }
    public DbSet<User>? User { get; set; }
}