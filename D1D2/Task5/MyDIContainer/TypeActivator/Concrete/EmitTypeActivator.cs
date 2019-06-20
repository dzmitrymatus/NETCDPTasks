using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using MyDIContainer.TypeActivator.Interface;

namespace MyDIContainer.TypeActivator.Concrete
{
    public class EmitTypeActivator : ITypeActivator
    {       
        public object ActivateInstance(Type type)
        {
            var dynamicMethod = new DynamicMethod($"{type.ToString()}_TypeActivator", type, null);
            var ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(Array.Empty<Type>()));
            ilGenerator.Emit(OpCodes.Ret);
            return dynamicMethod.Invoke(null, null);
        }

        public T ActivateInstance<T>()
        {
            return (T)ActivateInstance(typeof(T));
        }

        public object ActivateInstance(Type type, params object[] arguments)
        {
            Type[] parametersTypes = arguments.Select(p => p.GetType()).ToArray();
            DynamicMethod createMethod = new DynamicMethod($"{type.ToString()}_TypeActivator", type, parametersTypes);
            ILGenerator il = createMethod.GetILGenerator();
            for (int i = 0; i < arguments.Length; i++)
            {
                il.Emit(OpCodes.Ldarg, i);
            }

            ConstructorInfo ctor = type.GetConstructor(parametersTypes);
            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Ret);

            return createMethod.Invoke(null, arguments);
        }

        public T ActivateInstance<T>(params object[] arguments)
        {
            return (T)ActivateInstance(typeof(T), arguments);
        }
    }
}
