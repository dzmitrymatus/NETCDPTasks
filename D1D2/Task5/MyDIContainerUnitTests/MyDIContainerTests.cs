using MyDIContainer;
using MyDIContainer.Exceptions;
using MyDIContainerUnitTests.ExampleTypes.Logger;
using NUnit.Framework;

namespace MyDIContainerUnitTests
{
    [TestFixture]
    public class MyDIContainerTests
    {
        [Test]
        public void ResolveClassWithoutConstructorsTest()
        {
            #region Arrange
            var baseType = typeof(ILogger);
            var instanceType = typeof(SimpleLogger);

            var container = new Container();
            container.AddType(instanceType, baseType);
            #endregion

            #region Act
            var instance = container.CreateInstance(baseType);
            #endregion

            #region Assert
            Assert.That(instance != null,
                $"Container cant create object for '{baseType}' type.");
            Assert.That(instanceType.IsInstanceOfType(instance),
                $"Created instance has type '{instance.GetType()}' but should be '{instanceType}'.");
            #endregion
        }

        [Test]
        public void ResolveClassWithConstructorTest()
        {
            #region Arrange
            var baseType = typeof(ILogger);
            var instanceType = typeof(LoggerWithConstructor);
            var argumentType = typeof(ExampleClass);

            var container = new Container();
            container.AddType(argumentType);
            container.AddType(instanceType, baseType);
            #endregion

            #region Act
            var instance = container.CreateInstance(baseType);
            #endregion

            #region Assert
            Assert.That(instance != null,
                $"Container cant create object for '{baseType}' type.");
            Assert.That(instanceType.IsInstanceOfType(instance),
                $"Created instance has type '{instance.GetType()}' but should be '{instanceType}'.");
            #endregion
        }

        [Test]
        public void ResolveClassWithImportConstructorAttributeTest()
        {
            #region Arrange
            var baseType = typeof(ILogger);
            var instanceType = typeof(LoggerWithImportConstructor);
            var argumentType = typeof(ExampleClass);

            var container = new Container();
            container.AddType(argumentType);
            container.AddType(instanceType, baseType);
            #endregion

            #region Act
            var instance = container.CreateInstance(baseType);
            #endregion

            #region Assert
            Assert.That(instance != null,
                $"Container cant create object for '{baseType}' type.");
            Assert.That(instanceType.IsInstanceOfType(instance),
                $"Created instance has type '{instance.GetType()}' but should be '{instanceType}'.");
            #endregion
        }

        [Test]
        public void ResolveClassWithImportPropertyAttributeTest()
        {
            #region Arrange
            var baseType = typeof(ILogger);
            var instanceType = typeof(LoggerWithImportProperty);
            var propertyType = typeof(ExampleClass);

            var container = new Container();
            container.AddType(propertyType);
            container.AddType(instanceType, baseType);
            #endregion

            #region Act
            var instance = container.CreateInstance(baseType);
            #endregion

            #region Assert
            Assert.That(instance != null,
                $"Container cant create object for '{baseType}' type.");
            Assert.That(instanceType.IsInstanceOfType(instance),
                $"Created instance has type '{instance.GetType()}' but should be '{instanceType}'.");
            Assert.That(((LoggerWithImportProperty)instance).Item != null,
                $"Property must be not null.");
            #endregion
        }

        [Test]
        public void ExceptionWithUnregisteredObjectTest()
        {
            #region Arrange
            var baseType = typeof(ILogger);
            var instanceType = typeof(LoggerWithConstructor);
            //var argumentType = typeof(ExampleClass);

            var container = new Container();
            //container.AddType(argumentType);
            container.AddType(instanceType, baseType);
            #endregion

            #region Assert
            Assert.Throws<ContainerBindingException>(() => container.CreateInstance(baseType));
            #endregion
        }
    }
}
