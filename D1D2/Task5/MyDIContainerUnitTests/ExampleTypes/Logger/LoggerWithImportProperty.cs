using MyDIContainer.Attributes;

namespace MyDIContainerUnitTests.ExampleTypes.Logger
{
    public class LoggerWithImportProperty : ILogger
    {
        [ImportProperty]
        public ExampleClass Item { get; set; }
    }
}
