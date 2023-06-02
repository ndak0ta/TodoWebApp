using System;

namespace Todo.Infrastructure.Exceptions;

public class DuplicateRecordException : Exception
{
    public DuplicateRecordException() : base("Aynı kullanıcı adıyla başka bir kullanıcı zaten mevcut.")
    {
    }

    public DuplicateRecordException(string message) : base(message)
    {
    }
}


