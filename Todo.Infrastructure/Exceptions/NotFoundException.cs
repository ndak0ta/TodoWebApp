using System;

namespace Todo.Infrastructure.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Kayıt bulunamadı.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}

