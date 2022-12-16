namespace DesignPatterns.Bridge
{
    public class Bridge
    { }

    public class Abstraction
    {
        protected IImplementation _implementation;

        public Abstraction(IImplementation implementation)
        {
            _implementation = implementation;
        }

        public virtual string Operation()
        {
            return "Abstract: Base operation with: \n" + _implementation.OperationImplementation();
        }
    }

    public class ExtendedAbstraction : Abstraction
    {
        public ExtendedAbstraction(IImplementation implementation) : base(implementation)
        { }

        public override string Operation()
        {
            return "Extended abstraction: Extended operation with:\n" + _implementation.OperationImplementation();
        }
    }


    public interface IImplementation
    {
        string OperationImplementation();
    }

    class ConcreteImplementationA : IImplementation
    {
        public string OperationImplementation()
        {
            return "Concrete implementationA: The result in platform A.\n";
        }
    }

    class ConcreteImplementationB : IImplementation
    {
        public string OperationImplementation()
        {
            return "Concrete implementationB: The result in platform B.\n";
        }
    }

    public class Client
    {
        public void Main()
        {
            Abstraction abstraction;
            abstraction = new Abstraction(new ConcreteImplementationA());
            ClientCode(abstraction);

            Console.WriteLine();

            abstraction = new ExtendedAbstraction(new ConcreteImplementationB());
            ClientCode(abstraction);

        }

        public void ClientCode(Abstraction abstraction)
        {
            Console.WriteLine(abstraction.Operation());
        }
    }

}
