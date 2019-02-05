using System;

namespace MyDIContainer.Exceptions
{
    public class ContainerBindingException : Exception
    {
        public ContainerBindingException(string message) : base(message)
        { }
    }
}
