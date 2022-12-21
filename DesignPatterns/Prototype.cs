namespace DesignPatterns.Prototype
{
    public class Prototype
    { }

    public class Person
    {
        public int age;
        public DateTime birthDate;
        public string name;

        public IdInfo idInfo;

        public Person ShallowCopy()
        {
            return (Person)MemberwiseClone();
        }

        public Person DeepCopy()
        {
            Person clone = (Person)MemberwiseClone();
            clone.idInfo = new IdInfo(idInfo.IdNumber);
            clone.name = name;
            return clone;
        }
    }

    public class IdInfo
    {
        public int IdNumber;
        public IdInfo(int idNumber)
        {
            IdNumber = idNumber;
        }
    }

    public class Client
    {
        public void Main()
        {
            Person p1 = new Person();
            p1.age = 42;
            p1.birthDate = Convert.ToDateTime("1977-01-01");
            p1.name = "Jack";
            p1.idInfo = new IdInfo(313);

            Person p2 = p1.ShallowCopy();
            Person p3 = p1.DeepCopy();

            Console.WriteLine("Original values of p1, p2, p3:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values:");
            DisplayValues(p2);
            Console.WriteLine("   p3 instance values:");
            DisplayValues(p3);

            // Change p1
            p1.age = 32;
            p1.birthDate = Convert.ToDateTime("1900-01-01");
            p1.name = "Frank";
            p1.idInfo.IdNumber = 7878;

            Console.WriteLine("\nValues of p1, p2 and p3 after changes to p1:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values (reference values have changed):");
            DisplayValues(p2);
            Console.WriteLine("   p3 instance values (everything was kept the same):");
            DisplayValues(p3);

        }

        public static void DisplayValues(Person p)
        {
            Console.WriteLine("      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
                p.name, p.age, p.birthDate);
            Console.WriteLine("      ID#: {0:d}", p.idInfo.IdNumber);
        }
    }
}

namespace DesignPatterns.PrototypeV2
{
    public class CalendarPrototype
    {
        public virtual CalendarPrototype Clone()
        {
            return (CalendarPrototype)MemberwiseClone();
        }
    }

    public class CalendarEvent : CalendarPrototype
    {
        public Attendee[] Attendees { get; set; }
        public Priority Priority { get; set; }
        public DateTime StartDateAndTime { get; set; }

        public override CalendarPrototype Clone()
        {
            var copy = (CalendarEvent)base.Clone();
            var copiedAttendees = (Attendee[])Attendees.Clone();
            copy.Attendees = copiedAttendees;
            return copy;
        }
    }

    public enum Priority
    { 
        High,
        Low
    }

    public class Attendee
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Client
    {
        public static CalendarEvent GetExistingEvent()
        {
            var beerParty = new CalendarEvent();
            var friends = new Attendee[1];

            var person = new Attendee { Name = "Joe Schmoe" };

            friends[0] = person;

            beerParty.Attendees = friends;
            beerParty.StartDateAndTime = new DateTime(2022, 7, 23, 19, 0, 0);
            beerParty.Priority = Priority.High;

            return beerParty;
        }

        public void Main()
        {
            var beerParty = GetExistingEvent();
            var nextFridayEvent = (CalendarEvent)beerParty.Clone();
            nextFridayEvent.StartDateAndTime = new DateTime(2022, 7, 30, 19, 0, 0);

            nextFridayEvent.Attendees[0].Email = "person@gmail.com";
            nextFridayEvent.Priority = Priority.Low;
            if (beerParty.Attendees != nextFridayEvent.Attendees)
                Console.WriteLine("GOOD: Each event has own list of attendees.");

            if (beerParty.Attendees[0].Email == nextFridayEvent.Attendees[0].Email)
                Console.WriteLine("GOOD: Update to my e-mail address will be reflected in all events.");

            if (beerParty.Priority != nextFridayEvent.Priority)
                Console.WriteLine("GOOD: Each event should have own priority object, fully-copied.");
        }
    }
}
