using MyDIContainer.TypeActivator.Interface;
using System;

namespace MyDIContainer.TypeActivator.Concrete
{
    public class DefaultTypeActivator : ITypeActivator
    {
        public object ActivateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public T ActivateInstance<T>()
        {
            return (T)ActivateInstance(typeof(T));
        }

        public object ActivateInstance(Type type, params object[] arguments)
        {
            return Activator.CreateInstance(type, arguments);
        }

        public T ActivateInstance<T>(params object[] arguments)
        {
            return (T)ActivateInstance(typeof(T), arguments);
        }
    }
}
