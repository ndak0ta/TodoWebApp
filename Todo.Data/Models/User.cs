using System.ComponentModel.DataAnnotations;


namespace Todo.Data.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? userName { get; set; }

    [Required]
    public string? password { get; set; }

    public ICollection<TodoItem>? TodoItems { get; set; }
}


