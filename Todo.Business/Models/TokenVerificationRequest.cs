using System.Text.Json;


namespace Todo.Business.Models;

public class TokenVerificationRequest
{
    public string? Token { get; set; }
}