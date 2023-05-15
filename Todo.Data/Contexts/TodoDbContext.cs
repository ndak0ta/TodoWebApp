using Microsoft.EntityFrameworkCore;

namespace Todo.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }
    
    public DbSet<Todo> ToDo { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer("Server=localhost;Database=todo;User Id=sa;Password=123ASDzxc.;");
    // }
}