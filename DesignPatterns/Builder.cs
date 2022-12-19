namespace DesignPatterns.Builder
{
    public class Builder
    { }


    public interface IBuilder
    {
        void BuildPartA();
        void BuildPartB();
        void BuildPartC();
    }

    public class ConcreteBuilder : IBuilder
    {
        private Product _product = new Product();

        public ConcreteBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _product = new Product();
        }

        public void BuildPartA()
        {
            _product.Add("PartA1");
        }

        public void BuildPartB()
        {
            _product.Add("PartB1");
        }

        public void BuildPartC()
        {
            _product.Add("PartC1");
        }

        public Product GetProduct()
        {
            Product result = _product;
            Reset();
            return result;
        }
    }

    public class Product
    {
        private List<object> _parts = new();

        public void Add(string part)
        {
            _parts.Add(part);
        }

        public string ListParts()
        {
            string str = string.Empty;

            for (int i = 0; i < _parts.Count; i++)
            {
                str += _parts[i] + ", ";
            }

            str = str.Remove(str.Length - 2); 

            return "Product parts: " + str + "\n";
        }
    }

    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder
        {
            set { _builder = value; }
        }

        public void BuildMinimalViableProduct()
        {
            _builder.BuildPartA();
        }

        public void BuildFullFeaturedProduct()
        {
            _builder.BuildPartA();
            _builder.BuildPartB();
            _builder.BuildPartC();
        }
    }

    public class Client
    {
        public void Main()
        {
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;

            Console.WriteLine("Standart basic product:");
            director.BuildMinimalViableProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Standart full featured product:");
            director.BuildFullFeaturedProduct();
            Console.WriteLine(builder.GetProduct().ListParts());


            // Without Director pattern
            Console.WriteLine("Custom Product");
            builder.BuildPartA();
            builder.BuildPartC();
            Console.WriteLine(builder.GetProduct().ListParts());
        }
    }
}

namespace DesignPatterns.BuilderV2
{
    abstract class LaptopBuilder
    { 
        protected Laptop Laptop { get; private set; }
        public LaptopBuilder()
        {
            Laptop = new Laptop();
        }
        
        public void CreateNewLaptop() => Laptop = new Laptop();

        public Laptop GetLaptop() => Laptop;

        public abstract void SetMonitorResilution(string component);
        public abstract void SetProcessor(string component);
        public abstract void SetMemory(string component);
        public abstract void SetHDD(string component);
        public abstract void SetBattery(string component);
    }

    class GamingLaptopBuilder : LaptopBuilder
    {
        public override void SetBattery(string component)
        {
            Laptop.Battery = "6 lbs";
        }

        public override void SetHDD(string component)
        {
            Laptop.HDD = "500 Gb";
        }

        public override void SetMemory(string component)
        {
            Laptop.Memory = "6144 Mb";
        }

        public override void SetMonitorResilution(string component)
        {
            Laptop.MonitorResolution = "1920X1080";
        }

        public override void SetProcessor(string component)
        {
            Laptop.Processor = "Core 2 Duo, 3.2 GHz";
        }
    }

    class CustomLaptopBuilder : LaptopBuilder
    {
        public override void SetBattery(string component)
        {
            Laptop.Battery = component;
        }

        public override void SetHDD(string component)
        {
            Laptop.HDD = component;
        }

        public override void SetMemory(string component)
        {
            Laptop.Memory = component;
        }

        public override void SetMonitorResilution(string component)
        {
            Laptop.MonitorResolution = component;
        }

        public override void SetProcessor(string component)
        {
            Laptop.Processor = component;
        }
    }

    class Laptop
    {
        public string MonitorResolution { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string Memory { get; set; } = string.Empty;
        public string HDD { get; set; } = string.Empty;
        public string Battery { get; set; } = string.Empty;
        public override string ToString()
        {
            return $"{MonitorResolution} {Processor} {Battery} {HDD} {Memory}";
        }
    }

    class BuyLaptop
    {
        private LaptopBuilder _laptopBuilder;
        public BuyLaptop()
        {
            _laptopBuilder = new CustomLaptopBuilder();
        }

        public void SetLaptopBuilder(LaptopBuilder laptopBuilder)
        {
            _laptopBuilder = laptopBuilder;
        }

        public Laptop GetLaptop()
        {
            return _laptopBuilder.GetLaptop();
        }

        public void ConstructLaptop(string s1 = "default", string s2 = "default", string s3 = "default", string s4 = "default", string s5 = "default")
        {
            _laptopBuilder.CreateNewLaptop();
            _laptopBuilder.SetMonitorResilution(s1);
            _laptopBuilder.SetProcessor(s2);
            _laptopBuilder.SetBattery(s3);
            _laptopBuilder.SetHDD(s4);
            _laptopBuilder.SetMemory(s5);
        }
    }

    public class Client
    {
        public void Main()
        {
            var gamingBuilder = new GamingLaptopBuilder();
            var customBuilder = new CustomLaptopBuilder();
            var shop = new BuyLaptop();

            shop.SetLaptopBuilder(gamingBuilder);
            shop.ConstructLaptop();

            var laptop = shop.GetLaptop();

            shop.SetLaptopBuilder(customBuilder);
            shop.ConstructLaptop("a");

            var customLaptop = shop.GetLaptop();
            Console.WriteLine(laptop);
            Console.WriteLine();
            Console.WriteLine(customLaptop);
        }
    }
}


