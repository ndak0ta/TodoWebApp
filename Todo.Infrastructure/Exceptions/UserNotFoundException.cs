using System;

namespace Todo.Infrastructure.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Kayıt bulunamadı.")
    {
    }

    public UserNotFoundException(string message) : base(message)
    {
    }
}

