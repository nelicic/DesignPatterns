namespace DesignPatterns.Singleton
{
    public sealed class Singleton
    {
        private static Singleton? _instance;
        private Singleton() { }
        public static Singleton Instance()
        {
            if (_instance == null)
                _instance = new Singleton();
            else
                return _instance;

            return _instance;
        }

        public void SomeMethod() { }
    }

    public class Client
    {
        public void Main()
        {
            Singleton singleton1 = Singleton.Instance(); 
            Singleton singleton2 = Singleton.Instance();

            if (singleton1 == singleton2)
                Console.WriteLine("Singleton Works");
            else
                Console.WriteLine("Imposible");
        }
    }
}

namespace DesignPatterns.SingletonV2
{
    class LoggerSingleton
    {
        private LoggerSingleton() { }
        private int _logCount = 0;
        private static LoggerSingleton _instance = new LoggerSingleton();
        public static LoggerSingleton GetInstance()
        {
            if (_instance == null)
                _instance = new LoggerSingleton();
            return _instance;
        }

        public void Log(string message)
        {
            Console.WriteLine(_logCount + ": " + message);
            _logCount++;
        }
    }

    public class ThreadSafeLoggerSingleton
    {
        private ThreadSafeLoggerSingleton() { }

        private int _logCount = 0;
        private static ThreadSafeLoggerSingleton _instance;
        private static readonly object locker = new object();

        public static ThreadSafeLoggerSingleton GetInstance()
        {
            lock (locker)
            {
                if (_instance == null)
                    _instance = new ThreadSafeLoggerSingleton();
            }
            return _instance;
        }

        public void Log(string message)
        {
            Console.WriteLine(_logCount + ": " + message);
            _logCount++;
        }
    }

    public class HardProcessor
    {
        private int _start;
        public HardProcessor(int start)
        {
            _start = start;
            LoggerSingleton.GetInstance().Log("Processor just created.");
        }

        public int ProcessTo(int end)
        {
            int sum = 0;
            for (int i = _start; i <= end; i++)
            {
                sum += i;
            }

            LoggerSingleton.GetInstance().Log("Processor just calculated some value: " + sum);

            return sum;
        }
    }

    public class Client
    {
        public void Main()
        {
            LoggerSingleton logger = LoggerSingleton.GetInstance();
            HardProcessor processor = new HardProcessor(1);

            logger.Log("Hard work started...");
            processor.ProcessTo(5);
            logger.Log("Hard work finished...");
        }
    }
}