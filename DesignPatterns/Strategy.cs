namespace DesignPatterns.Strategy
{
    class Context
    {
        private IStrategy _strategy;

        public Context()
        { }

        public Context(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");
            var result = _strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Empty;
            foreach (var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }
    }

    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    public class Client
    {
        public void Main()
        {
            var context = new Context();

            Console.WriteLine("Client: Strategy is set to normal sorting.");
            context.SetStrategy(new ConcreteStrategyA());
            context.DoSomeBusinessLogic();

            Console.WriteLine();

            Console.WriteLine("Client: Strategy is set to reverse sorting.");
            context.SetStrategy(new ConcreteStrategyB());
            context.DoSomeBusinessLogic();
        }
    }
}


namespace DesignPatterns.StrategyV2
{
    class Myself
    {
        private IWearingStrategy _wearingStrategy = new SunshineWearingStrategy();
        public void ChangeStrategy(IWearingStrategy wearingStrategy)
        {
            _wearingStrategy = wearingStrategy;
        }
        public void GoOutside()
        {
            var clothes = _wearingStrategy.GetClothes();
            var accessories = _wearingStrategy.GetAccessories();
            Console.WriteLine("Today I wore {0} and took {1}", clothes, accessories);
        }
    }

    interface IWearingStrategy
    {
        string GetClothes();
        string GetAccessories();
    }

    class SunshineWearingStrategy : IWearingStrategy
    {
        public string GetClothes() => "T-Shirt";
        public string GetAccessories() => "sunglasses";
    }

    public class Client
    {
        public void Main()
        {
            var me = new Myself();
            me.ChangeStrategy(new SunshineWearingStrategy());
            me.GoOutside();
        }
    }

    internal class RainWearingStrategy : IWearingStrategy
    {
        public string GetAccessories() => "Umbrella";

        public string GetClothes() => "Coat";
    }
}