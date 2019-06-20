using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using MyDIContainer.Attributes;
using MyDIContainer.Exceptions;
using MyDIContainer.TypeActivator.Interface;
using MyDIContainer.TypeActivator.Concrete;
using MyDIContainer.Extensions;

namespace MyDIContainer
{
    public class Container
    {
        #region Fields
        private Dictionary<Type, Type> _typesDictionary;
        private ITypeActivator _typeActivator;
        #endregion

        #region Constructors
        public Container() : this(new EmitTypeActivator())
        {           
        }

        public Container(ITypeActivator typeActivator)
        {
            _typesDictionary = new Dictionary<Type, Type>();
            _typeActivator = typeActivator;
        }
        #endregion

        #region Add Methods
        public void AddAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => Attribute.IsDefined(type, typeof(ExportAttribute)));
            foreach(var type in types)
            {
                _typesDictionary.Add(type.GetCustomAttribute<ExportAttribute>().BaseType?? type, type);
            }
        }

        public void AddType<T>()
        {
            AddType(typeof(T), typeof(T));
        }

        public void AddType<T, BaseT>()
        {
            AddType(typeof(T), typeof(BaseT));
        }

        public void AddType(Type type)
        {
            AddType(type, type);
        }

        public void AddType(Type type, Type baseType)
        {
            if (_typesDictionary.ContainsKey(baseType))
            {
                _typesDictionary.Remove(baseType);
            }
            _typesDictionary.Add(baseType, type);
        }
        #endregion

        #region Create Methods
        public object CreateInstance(Type type)
        {
            if (_typesDictionary.ContainsKey(type) == false)
            {
                throw new ContainerBindingException($"Container does not have any binding for '{type.ToString()}' type!");
            }

            if (_typesDictionary[type].IsTypeHasAttribute<ImportConstructorAttribute>())
            {
                return CreateUsingConstructor(_typesDictionary[type]);
            }

            if (_typesDictionary[type].IsTypeHasPropertiesWithAttribute<ImportPropertyAttribute>())
            {
                return CreateUsingProperties(_typesDictionary[type]);
            }

            return CreateUsingConstructor(_typesDictionary[type]);
        }

        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        private object CreateUsingConstructor(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
            {
                return _typeActivator.ActivateInstance(type);
            }

            foreach (var constructor in type.GetConstructors().OrderBy(x => x.GetParameters().Count()))
            {
                List<object> parametersInstances = new List<object>();

                foreach (var parameter in constructor.GetParameters())
                {
                    var parameterInstance = CreateInstance(parameter.ParameterType);
                    if(parameterInstance == null)
                    {
                        parametersInstances = null;
                        break;
                    }
                    parametersInstances.Add(parameterInstance);
                }

                if(parametersInstances != null)
                {
                    return _typeActivator.ActivateInstance(type, parametersInstances.ToArray());
                }
            }

            return null;
        }

        private object CreateUsingProperties(Type type)
        {
            var instance = _typeActivator.ActivateInstance(type);
            var propertiesToSet = type.GetProperties().Where(x => Attribute.IsDefined(x, typeof(ImportPropertyAttribute)));
            foreach (var propertyToSet in propertiesToSet)
            {
                propertyToSet.SetValue(instance, CreateInstance(propertyToSet.PropertyType));
            }
            return instance;
        }
        #endregion
    }
}
