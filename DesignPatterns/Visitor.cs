namespace DesignPatterns.Visitor
{
    public interface IComponent
    {
        void Accept(IVisitor visitor);
    }

    public class ConcreteComponentA : IComponent
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentA(this);
        }

        public string ExclusiveMethodOfConcreteComponentA()
        {
            return "A";
        }
    }

    public class ConcreteComponentB : IComponent
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentB(this);
        }

        public string SpecialMethodOfConcreteComponentB()
        {
            return "B";
        }
    }

    public interface IVisitor
    {
        void VisitConcreteComponentA(ConcreteComponentA element);

        void VisitConcreteComponentB(ConcreteComponentB element);
    }
    class ConcreteVisitor1 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor1");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor1");
        }
    }

    class ConcreteVisitor2 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor2");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor2");
        }
    }

    public class Client
    {
        public void Main()
        {
            List<IComponent> components = new List<IComponent>
            {
                new ConcreteComponentA(),
                new ConcreteComponentB()
            };

            Console.WriteLine("The client code works with all visitors via the base Visitor interface:");
            var visitor1 = new ConcreteVisitor1();
            ClientCode(components, visitor1);

            Console.WriteLine();

            Console.WriteLine("It allows the same client code to work with different types of visitors:");
            var visitor2 = new ConcreteVisitor2();
            ClientCode(components, visitor2);
        }

        public static void ClientCode(List<IComponent> components, IVisitor visitor)
        {
            foreach (var component in components)
            {
                component.Accept(visitor);
            }
        }
    }

}


/*namespace DesignPatterns.VisitorV2
{
    interface IVisitor
    {
        void Visit(OfficeBuilding building);
        void Visit(Floor floor);
        void Visit(Room room);
    }
    interface IElement
    {
        void Accept(IVisitor visitor);
    }

    public class OfficeBuilding
    {
        public int ElectricitySystemId { get; set; }
        public string BuildingName { get; set; }
    }

    class Floor : IElement
    {
        private readonly IList<Room> _rooms = new List<Room>();
        public int FloorNumber { get; private set; }
        public IEnumerable<Room> Rooms { get { return _rooms; } }
        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
        }
        public void AddRoom(Room room)
        {
            _rooms.Add(room);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var room in Rooms)
            {
                room.Accept(visitor);
            }
        }
    }

    public class Room
    {
        public int RoomNumber { get; set; }
    }

    class ElectricitySystemValidator : IVisitor
    {
        public void Visit(OfficeBuilding building)
        {
            var electricityState = (building.ElectricitySystemId > 1000)
            ? "Good" : "Bad";
            Console.WriteLine(
            string.Format("Main electric shield in building {0} is in {1} state.", building.BuildingName, electricityState));
        }
        public void Visit(Floor floor)
        {
            Console.WriteLine(
            string.Format("Diagnosting electricity on floor {0}.", floor.FloorNumber));
        }
        public void Visit(Room room)
        {
            Console.WriteLine(string.Format("Diagnosting electricity in room {0}.", room.RoomNumber));
        }
    }

    public class Client
    {
        public void Main()
        {
            var floor1 = new Floor(1);
            floor1.AddRoom(new Room(100));
            floor1.AddRoom(new Room(101));
            floor1.AddRoom(new Room(102));
            var floor2 = new Floor(2);
            floor2.AddRoom(new Room(200));
            floor2.AddRoom(new Room(201));
            floor2.AddRoom(new Room(202));
            var myFirmOffice = new OfficeBuilding("[Design Patterns Center]", 25, 990);
            myFirmOffice.AddFloor(floor1);
            myFirmOffice.AddFloor(floor2);
            var electrician = new ElectricitySystemValidator();
            myFirmOffice.Accept(electrician);
            var plumber = new PlumbingSystemValidator();
            myFirmOffice.Accept(plumber);
        }
    }
}*/