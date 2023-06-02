using System;

namespace Todo.Infrastructure.Exceptions;

public class DuplicateRecordException : Exception
{
    public DuplicateRecordException() : base("Tekrarlı kayıt hatası")
    {
    }

    public DuplicateRecordException(string message) : base(message)
    {
    }
}


