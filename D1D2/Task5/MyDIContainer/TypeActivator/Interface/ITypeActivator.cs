using System;

namespace MyDIContainer.TypeActivator.Interface
{
    public interface ITypeActivator
    {
        object ActivateInstance(Type type);
        T ActivateInstance<T>();
        object ActivateInstance(Type type, params object[] arguments);
        T ActivateInstance<T>(params object[] arguments);
    }
}
