using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Todo.Data.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; } 
    
    public string? Header { get; set; } 
    
    public string? Body { get; set; } 
    
    public DateTime Date { get; set; }
}