using MyDIContainer.Attributes;

namespace MyDIContainerUnitTests.ExampleTypes.Logger
{
    [ImportConstructor]
    public class LoggerWithImportConstructor : ILogger
    {
        public LoggerWithImportConstructor(ExampleClass item)
        {

        }
    }
}
