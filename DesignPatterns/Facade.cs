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

namespace DesignPatterns.FacadeV2
{
    class SkiRent
    {
        public int RentBoots(int feetSize, int skierLevel)
        {
            return 20;
        }

        public int RentSki(int weight, int skierLevel)
        {
            return 40;
        }
        public int RentPole(int height)
        {
            return 5;
        }
    }

    class SkiResortTicketSystem
    {
        public int BuyOneDayTicket()
        {
            return 115;
        }

        public int BuyHalfDayTicket()
        {
            return 60;
        }
    }

    class HotelBookingSystem
    {
        public int BookRoom(int roomQuality)
        {
            switch (roomQuality)
            {
                case 3:
                    return 250;
                case 4:
                    return 500;
                case 5:
                    return 900;
                default:
                    throw new ArgumentException("Room quality should be in range [3..5]");
            }
        }
    }

    class SkiResortFacade
    {
        private SkiRent _skiRent = new SkiRent();
        private SkiResortTicketSystem _skiResortTicketSystem = new SkiResortTicketSystem();

        private HotelBookingSystem hotelBookingSystem = new HotelBookingSystem();

        public int HaveGoodRest(int height, int weight, int feetSize, int skierLevel, int roomQuality)
        {
            int skiPrice = _skiRent.RentSki(weight, skierLevel);
            int skiBootsPrice = _skiRent.RentBoots(feetSize, skierLevel);
            int polePrice = _skiRent.RentPole(height);

            int oneDayTicketPr = _skiResortTicketSystem.BuyOneDayTicket();
            int hotelPrice = hotelBookingSystem.BookRoom(roomQuality);

            return skiBootsPrice + oneDayTicketPr + hotelPrice + skiPrice + polePrice;
        }
    }
}