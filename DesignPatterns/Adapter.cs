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