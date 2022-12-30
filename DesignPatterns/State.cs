namespace DesignPatterns.State
{
    class Context
    {
        private State _state = null;

        public Context(State state)
        {
            TransitionTo(state);
        }

        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            _state = state;
            _state.SetContext(this);
        }

        public void Request1()
        {
            _state.Handle1();
        }

        public void Request2()
        {
            _state.Handle2();
        }
    }

    abstract class State
    {
        protected Context _context;

        public void SetContext(Context context)
        {
            _context = context;
        }

        public abstract void Handle1();

        public abstract void Handle2();
    }

    class ConcreteStateA : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateA handles request1.");
            Console.WriteLine("ConcreteStateA wants to change the state of the context.");
            _context.TransitionTo(new ConcreteStateB());
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateA handles request2.");
        }
    }

    class ConcreteStateB : State
    {
        public override void Handle1()
        {
            Console.Write("ConcreteStateB handles request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateB handles request2.");
            Console.WriteLine("ConcreteStateB wants to change the state of the context.");
            _context.TransitionTo(new ConcreteStateA());
        }
    }

    public class Client
    {
        public void Main()
        {
            var context = new Context(new ConcreteStateA());
            context.Request1();
            context.Request2();
        }
    }
}


/*namespace DesignPatterns.StateV2
{
    public class Product
    { }
    public class Order
    {
        private OrderState _state;
        private List<Product> _products = new List<Product>();
        public Order() => _state = new NewOrder(this);
        public void SetOrderState(OrderState state) => _state = state;
        public void WriteCurrentStateName() => Console.WriteLine("Current Order's state: {0}", _state.GetType().Name);
        public void Ship() => _state.Ship();
    }

    class OrderState
    {
        public Order _order;
        public OrderState(Order order)
        {
            _order = order;
        }
        public virtual void AddProduct()
        {
            OperationIsNotAllowed("AddProduct");
        }
        private void OperationIsNotAllowed(string operationName)
        {
            Console.WriteLine("Operation {0} is not allowed for Order's state {1}",
            operationName, GetType().Name);
        }
    }

    class Granted : OrderState
    {
        public Granted(Order order)
        : base(order)
        { }
        public override void AddProduct()
        {
            _order.DoAddProduct();
        }
        public override void Ship()
        {
            _order.DoShipping();
            _order.SetOrderState(new Shipped(_order));
        }
        public override void Cancel()
        {
            _order.DoCancel();
            _order.SetOrderState(new Cancelled(_order));
        }
    }


    class Client
    { 
        public void Main()
        {
            Product beer = new Product();
            beer.Name = "MyBestBeer";
            beer.Price = 78000;
            Order order = new Order();
            order.WriteCurrentStateName();
            order.AddProduct(beer);
            order.WriteCurrentStateName();
            order.Register();
            order.WriteCurrentStateName();
            order.Grant();
            order.WriteCurrentStateName();
            order.Ship();
            order.WriteCurrentStateName();

        }
    }
}*/