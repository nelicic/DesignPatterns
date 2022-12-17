namespace DesignPatterns.Facade
{
    public class Facade
    {
        protected Subsystem1 _subsystem1;

        protected Subsystem2 _subsystem2;

        public Facade(Subsystem1 s1, Subsystem2 s2)
        {
            _subsystem1 = s1;
            _subsystem2 = s2;
        }

        public string Operation()
        {
            string result = "Facade initializes subsystems:\n";
            result += _subsystem1.Operation1();
            result += _subsystem2.Operation1();
            result += "Facade orders subsystems to perform the action:\n";
            result += _subsystem1.OperationN();
            result += _subsystem2.OperationX();
            return result;
        }
    }

    public class Subsystem1
    {
        public string Operation1()
        {
            return "Subsystem1: Ready!\n";
        }

        public string OperationN()
        {
            return "Subsystem1: Go!\n";
        }
    }

    public class Subsystem2
    {
        public string Operation1()
        {
            return "Subsystem2: Ready!\n";
        }

        public string OperationX()
        {
            return "Subsystem2: Go!\n";
        }
    }

    public class Client
    {
        public void Main()
        {
            Facade facade = new Facade(new Subsystem1(), new Subsystem2());
            Console.WriteLine(facade.Operation());
        }
    }
}
