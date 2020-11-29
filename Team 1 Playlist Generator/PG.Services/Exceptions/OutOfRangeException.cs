using System;

namespace PG.Services.Exceptions
{
    public class OutOfRangeException : Exception
    {
        public OutOfRangeException(string message) : base(message)
        { }
    }
}
