using System;

namespace Clients.MyExceptions
{
    public class AgeExceptions : ArgumentException
    {
        public AgeExceptions(string message)
            : base(message)
        { }
    }
}