using System;

namespace MyDIContainer.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ImportConstructorAttribute : Attribute
    {
    }
}
