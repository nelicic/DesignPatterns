namespace DesignPatterns.ChainOfResponsibility
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }

    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
                return _nextHandler.Handle(request);
            return null;
        }
    }

    class MonkeyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "Banana")
            {
                return $"Monkey: I'll eat the {request}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class SquirrelHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Nut")
            {
                return $"Squirrel: I'll eat the {request}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class DogHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "MeatBall")
            {
                return $"Dog: I'll eat the {request}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class Client
    {
        public void Main()
        {
            var monkey = new MonkeyHandler();
            var squirrel = new SquirrelHandler();
            var dog = new DogHandler();


            monkey.SetNext(squirrel).SetNext(dog);

            Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
            ClientCode(monkey);
            Console.WriteLine();

            Console.WriteLine("Subchain: Squirrel > Dog\n");
            ClientCode(squirrel);
        }

        public static void ClientCode(AbstractHandler handler)
        {
            foreach (var food in new List<string> { "Nut", "Banana", "Cup of coffee" })
            {
                Console.WriteLine($"Client: Who wants a {food}?");

                var result = handler.Handle(food);

                if (result != null)
                {
                    Console.Write($"   {result}");
                }
                else
                {
                    Console.WriteLine($"   {food} was left untouched.");
                }
            }
        }
    }
}

namespace DesignPatterns.ChainOfResponsibilityV2
{
    public abstract class WierdCafeVisitor
    {
        public WierdCafeVisitor CafeVisitor { get; private set; }
        protected WierdCafeVisitor(WierdCafeVisitor cafeVisitor)
        {
            CafeVisitor = cafeVisitor;
        }
        public virtual void HandleFood(Food food)
        {
            if (CafeVisitor != null)
            {
                CafeVisitor.HandleFood(food);
            }
        }
    }

    public class BestFriend : WierdCafeVisitor
    {
        public List<Food> CoffeeContainingFood { get; private set; }
        public BestFriend(WierdCafeVisitor cafeVisitor) : base(cafeVisitor)
        {
            CoffeeContainingFood = new List<Food>();
        }

        public override void HandleFood(Food food)
        {
            if (food.Ingredients.Contains("Meat"))
            {
                Console.WriteLine("BestFriend: I just ate {0}. It was tasty.", food.Name);
                return;
            }

            if (food.Ingredients.Contains("Coffee") && CoffeeContainingFood.Count < 1)
            {
                CoffeeContainingFood.Add(food);
                Console.WriteLine("BestFriend: I have to take something with coffee. {0} looks fine.", food.Name);
                return;
            }
        }
    }

    public class GirlFriend : WierdCafeVisitor
    {
        public GirlFriend(WierdCafeVisitor cafeVisitor) : base(cafeVisitor)
        { }

        public override void HandleFood(Food food)
        {
            if (food.Name == "Cappucino")
            {
                Console.WriteLine("Girlfriend: My lovely cappucino!!!");
                return;
            }
            base.HandleFood(food);
        }
    }

    public class Me : WierdCafeVisitor
    {
        public Me(WierdCafeVisitor cafeVisitor) : base(cafeVisitor)
        { }

        public override void HandleFood(Food food)
        {
            if (food.Name == "Soup with potato")
            {
                Console.WriteLine("Woww PATATA!!!");
                return;
            }
            base.HandleFood(food);
        }
    }

    public class Food
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public Food(string name, List<string> ingredients)
        {
            Name = name;
            Ingredients = ingredients;
        }
    }

    public class Client
    { 
        public void Main()
        {
            var cappuccino1 = new Food("Cappuccino", new List<string> {"Coffee", "Milk", "Sugar"});
            var cappuccino2 = new Food("Cappuccino", new List<string> { "Coffee", "Milk" });
            var soup1 = new Food("Soup with meat", new List<string> {"Meat", "Water", "Potato"});
            var soup2 = new Food("Soup with potato", new List<string> { "Water", "Potato" });
            var meat = new Food("Meat", new List<string> { "Meat" });
            var girlFriend = new GirlFriend(null);
            var me = new Me(girlFriend);
            var bestFriend = new BestFriend(me);
            bestFriend.HandleFood(cappuccino1);
            bestFriend.HandleFood(cappuccino2);
            bestFriend.HandleFood(soup1);
            bestFriend.HandleFood(soup2);
            bestFriend.HandleFood(meat);
        }
    }
}