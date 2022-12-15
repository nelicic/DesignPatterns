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
