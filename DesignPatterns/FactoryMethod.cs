namespace DesignPatterns.FactoryMethod
{
    public class FactoryMethod
    { }

    public abstract class Creator
    {
        public abstract IProduct FactoryMethod();
        public string SomeOperation()
        {
            var product = FactoryMethod();
            var result = "Creator: The same creator's code has just worked with " + product.Operation();
            return result;
        }
    }

    public class ConcreteCreator1 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct1();
        }
    }

    public class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct2();
        }
    }

    public interface IProduct
    {
        string Operation();
    }

    class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "Result of Concrete product 1";
        }
    }

    class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "Result of Concrete product 2";
        }
    }

    class Client
    { 
        public void Main()
        {
            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new ConcreteCreator1());

            Console.WriteLine("");

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new ConcreteCreator2());
        }

        private void ClientCode(Creator creator)
        {
            Console.WriteLine("Client: I'm not aware of the creator's class," +
                "but it still works.\n" + creator.SomeOperation());
        }
    }
}

namespace DesignPatterns.FactoryMethodV2
{
    public interface ILogger
    {
        void LogMessage(string message);
        void LogError(string message);
        void LogVerboseInformation(string message);
    }

    class Log4NetLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(string.Format("{0}: {1}", "Log4Net", message));
        }
        public void LogError(string message) => throw new NotImplementedException();
        public void LogVerboseInformation(string message) => throw new NotImplementedException();
    }

    class EnterpriseLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(string.Format("{0}: {1}", "Enterprise", message));
        }
        public void LogError(string message) => throw new NotImplementedException();
        public void LogVerboseInformation(string message) => throw new NotImplementedException();
    }

    class LoggerProviderFactory
    {
        public static ILogger GetLoggingProvider(LoggingProviders logProviders)
        {
            switch (logProviders)
            {
                case LoggingProviders.Enterprise:
                    return new EnterpriseLogger();
                case LoggingProviders.Log4Net:
                    return new Log4NetLogger();
                default:
                    return new EnterpriseLogger();
            }
        }
    }
    public enum LoggingProviders
    {
        Enterprise,
        Log4Net
    }

    class Client
    {
        public void Main()
        {
            var providerType = GetTypeOfLoggingProviderFromConfigFile();
            ILogger logger = LoggerProviderFactory.GetLoggingProvider(providerType);
            logger.LogMessage("Factory Method Design Pattern.");
        }

        private static LoggingProviders GetTypeOfLoggingProviderFromConfigFile()
        {
            return LoggingProviders.Log4Net;
        }
    }
}