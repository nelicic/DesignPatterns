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


namespace DesignPatterns.BridgeV2
{
    interface IBuildingCompany
    {
        void BuildFoundation();
        void BuildRoom();
        void BuildRoof();
        IWallCreator WallCreator { get; set; }
    }

    interface IWallCreator
    {
        public void BuildWallWithDoor();
        public void BuildWall();
        public void BuildWallWithWindow();
    }

    public class SlabWallCreator : IWallCreator
    {
        public void BuildWall()
        {
            Console.WriteLine("Concrete slab wall with door.");
        }

        public void BuildWallWithDoor()
        {
            Console.WriteLine("Concrete slab wall with door.");
        }

        public void BuildWallWithWindow()
        {
            Console.WriteLine("Concrete slab wall with window.");
        }
    }

    public class BrickWallCreator : IWallCreator
    {
        public void BuildWall()
        {
            Console.WriteLine("Concrete brick wall with door.");
        }

        public void BuildWallWithDoor()
        {
            Console.WriteLine("Concrete brick wall with door.");
        }

        public void BuildWallWithWindow()
        {
            Console.WriteLine("Concrete brick wall with window.");
        }
    }

    class BuildingCompany : IBuildingCompany
    {
        public IWallCreator WallCreator { get; set; }

        public void BuildFoundation()
        {
            Console.WriteLine("Foundation is built.{0}", Environment.NewLine);
        }

        public void BuildRoof()
        {
            Console.WriteLine("Roof is done.{0}", Environment.NewLine);
        }

        public void BuildRoom()
        {
            WallCreator.BuildWallWithDoor();
            WallCreator.BuildWall();
            WallCreator.BuildWallWithWindow();
            WallCreator.BuildWall();
            Console.WriteLine("Room finished.{0}", Environment.NewLine);
        }
    }

    public class Client
    {
        public void Main()
        {
            var brickWallCreator = new BrickWallCreator();
            var concreteSlabWallCreator = new SlabWallCreator();

            var buildingCompany = new BuildingCompany();
            buildingCompany.BuildFoundation();

            buildingCompany.WallCreator = concreteSlabWallCreator;
            buildingCompany.BuildRoom();

            buildingCompany.WallCreator = brickWallCreator;
            buildingCompany.BuildRoom();
            buildingCompany.BuildRoom();

            buildingCompany.BuildRoof();
        }
    }
}
