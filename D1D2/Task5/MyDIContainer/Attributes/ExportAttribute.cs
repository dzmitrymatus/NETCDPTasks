using System;

namespace MyDIContainer.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportAttribute : Attribute
    {
        public Type BaseType;

        public ExportAttribute()
        {
            BaseType = null;
        }

        public ExportAttribute(Type type)
        {
            BaseType = type;
        }
    }
}
