namespace DesignPatterns.Adapter
{
    public class Adaptee
    {
        public string StrNum()
        {
            return "145";
        }
    }

    public interface ITarget
    {
        int GetNum();
    }

    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }

        public int GetNum()
        {
            return int.Parse(_adaptee.StrNum());
        }
    }

    public class Client
    { 
        public void Main()
        {
            Adaptee adaptee = new Adaptee();
            ITarget target = new Adapter(adaptee);

            int x = 0;

            // x = adaptee.StrNum(); error

            Console.WriteLine("Adaptee interface is incompatible with the client.");
            Console.WriteLine("But with adapter client can call it's method.");

            x = target.GetNum();

            Console.WriteLine(x);
        }
    }
}

namespace DesignPatterns.AdapterV2
{
    class OldElectricitySystem
    {
        public string MatchThinSocket()
        {
            return "220V - old electricity system";
        }
    }

    interface INewElectricitySystem
    {
        public string MatchWideSocket();
    }

    class NewElectricitySystem : INewElectricitySystem
    {
        public string MatchWideSocket()
        {
            return "220V - new electricity system";
        }
    }

    class Adapter : INewElectricitySystem
    {
        private readonly OldElectricitySystem _adtaptee;
        public Adapter(OldElectricitySystem adaptee)
        {
            _adtaptee = adaptee;
        }
        public string MatchWideSocket()
        {
            return _adtaptee.MatchThinSocket();
        }
    }

    class ElectricityConsumer
    {
        public static void ChargeNotebook(INewElectricitySystem electricitySystem)
        {
            Console.WriteLine(electricitySystem.MatchWideSocket());
        }
    }

    class Client
    {
        public void Main()
        {
            var newElectricitySystem = new NewElectricitySystem();
            ElectricityConsumer.ChargeNotebook(newElectricitySystem);

            var oldElectricitySystem = new OldElectricitySystem();
            Adapter adapter = new Adapter(oldElectricitySystem);
            ElectricityConsumer.ChargeNotebook(adapter);
        }
    }
}