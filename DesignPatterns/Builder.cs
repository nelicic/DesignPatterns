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
