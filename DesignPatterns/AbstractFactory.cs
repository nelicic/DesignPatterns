namespace DesignPatterns.AbstractFactory
{
    public class AbstractFactory
    { }

    public interface IChair
    {
        public void DisplayChair();
    }

    public class VictorianChair : IChair
    {
        public void DisplayChair() => Console.WriteLine("Victorian Chair");
    }

    public class ModernChair : IChair
    {
        public void DisplayChair() => Console.WriteLine("Modern Chair");
    }

    public class ArDecoChair : IChair
    {
        public void DisplayChair() => Console.WriteLine("ArDeco Chair");
    }

    public interface ICouch
    {
        public void DisplayCouch();
    }

    public class VictorianCouch : ICouch
    {
        public void DisplayCouch() => Console.WriteLine("Victorian Couch");
    }

    public class ModernCouch : ICouch
    {
        public void DisplayCouch() => Console.WriteLine("Modern Couch");
    }

    public class ArDecoCouch : ICouch
    {
        public void DisplayCouch() => Console.WriteLine("ArDeco Couch");
    }

    public interface IArmChair
    {
        public void DisplayArmChair();
    }

    public class VictorianArmChair : IArmChair
    {
        public void DisplayArmChair() => Console.WriteLine("Victorian Armchair");
    }

    public class ModernArmChair : IArmChair
    {
        public void DisplayArmChair() => Console.WriteLine("Modern Armchair");
    }

    public class ArDecoArmChair : IArmChair
    {
        public void DisplayArmChair() => Console.WriteLine("ArDeco Armchair");
    }


    public interface IAbstractFactory
    {
        IChair CreateChair();
        ICouch CreateCouch();
        IArmChair CreateArmChair();
    }

    public class VictorianFactory : IAbstractFactory
    {
        public IArmChair CreateArmChair() => new VictorianArmChair();
        public IChair CreateChair() => new VictorianChair();
        public ICouch CreateCouch() => new VictorianCouch();
    }

    public class ArDecoFactory : IAbstractFactory
    {
        public IArmChair CreateArmChair() => new ArDecoArmChair();
        public IChair CreateChair() => new ArDecoChair();
        public ICouch CreateCouch() => new ArDecoCouch();
    }

    public class ModernFactory : IAbstractFactory
    {
        public IArmChair CreateArmChair() => new ModernArmChair();
        public IChair CreateChair() => new ModernChair();
        public ICouch CreateCouch() => new ModernCouch();
    }


    public class Client
    {
        public void Main()
        {
            Console.WriteLine("Client: Testing client code with the Victorian factory type...");
            ClientMethod(new VictorianFactory());
            Console.WriteLine();

            Console.WriteLine("Client: Testing client code with the ArDeco factory type...");
            ClientMethod(new ArDecoFactory());
            Console.WriteLine();

            Console.WriteLine("Client: Testing client code with the Modern factory type...");
            ClientMethod(new ModernFactory());
            Console.WriteLine();
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            var chair = factory.CreateChair();
            var armChair = factory.CreateArmChair();
            var couch = factory.CreateCouch();

            chair.DisplayChair();
            armChair.DisplayArmChair();
            couch.DisplayCouch();
        }
    }
}


namespace DesignPatterns.AbstractFactoryV2
{
    public interface IToyFactory
    {
        Bear GetBear();
        Cat GetCat();
    }

    public abstract class AnimalToy
    {
        public string Name { get; set; }
        public AnimalToy(string name) => Name = name;  
    }

    public abstract class Cat : AnimalToy
    {
        protected Cat(string name) : base(name) { }
    }

    public abstract class Bear : AnimalToy
    {
        protected Bear(string name) : base(name) { }
    }

    public class WoodenBear : Bear
    {
        public WoodenBear(string name = "Wooden Bear") : base(name) { }
    }

    public class WoodenCat : Cat
    {
        public WoodenCat(string name = "Wooden Cat") : base(name) { }
    }

    public class TeddyBear : Bear
    {
        public TeddyBear(string name = "Teddy Bear") : base(name) { }
    }
    
    public class TeddyCat : Cat
    {
        public TeddyCat(string name = "Teddy Cat") : base(name) { }
    }

    public class TeddyFactory : IToyFactory
    {
        public Bear GetBear()
        {
            return new TeddyBear();
        }

        public Cat GetCat()
        {
            return new TeddyCat();
        }
    }

    public class WoodenFactory : IToyFactory
    {
        public Bear GetBear()
        {
            return new WoodenBear();
        }

        public Cat GetCat()
        {
            return new WoodenCat();
        }
    }

    public class Client
    {
        public void Main()
        {
            IToyFactory factory = new TeddyFactory();
            Bear bear = factory.GetBear();
            Cat cat = factory.GetCat();
            Console.WriteLine(bear.Name);
            Console.WriteLine(cat.Name);


            factory = new WoodenFactory();
            bear = factory.GetBear();
            cat = factory.GetCat();
            Console.WriteLine(bear.Name);
            Console.WriteLine(cat.Name);
        }
    }
}