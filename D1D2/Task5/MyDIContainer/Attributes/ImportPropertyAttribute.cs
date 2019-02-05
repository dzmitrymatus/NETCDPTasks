using System;

namespace MyDIContainer.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ImportPropertyAttribute : Attribute
    {
    }
}
