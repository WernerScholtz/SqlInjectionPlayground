using System;

namespace SqlInjectionDemo.Common
{
    public class FunSpoilerException : Exception
    {
        public FunSpoilerException(string message) : base(message)
        {
        }
    }
}
