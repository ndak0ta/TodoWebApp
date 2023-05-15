using System.ComponentModel.DataAnnotations;

namespace Todo.Data;

public class Todo
{
    [Key]
    public int Id { get; set; } 
    
    public string? Header { get; set; } 
    
    public string? Body { get; set; } 
    
    public DateTime Date { get; set; } 
}