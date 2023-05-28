using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Data.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; } 
    
    public string? Header { get; set; } 
    
    public string? Body { get; set; } 
    
    public DateTime Date { get; set; }

    [ForeignKey("User")]
    public int userId { get; set; }

    public User? User { get; set; }
}