namespace DesignPatterns.Observer
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();
    }

    public class Subject : ISubject
    {
        public int State { get; set; } = -0;

        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nSubject: I'm doing something important.");
            State = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Subject: My state has just changed to: " + State);
            Notify();
        }
    }

    class ConcreteObserverA : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State < 3)
            {
                Console.WriteLine("ConcreteObserverA: Reacted to the event.");
            }
        }
    }

    class ConcreteObserverB : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
            {
                Console.WriteLine("ConcreteObserverB: Reacted to the event.");
            }
        }
    }

    public class Client
    {
        public void Main()
        {
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
        }
    }
}

namespace DesignPatterns.ObserverV2
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }

    class RiskyPlayer : IObserver
    {
        public string BoxerToPutMoneyOn { get; set; }
        public void Update(ISubject subject)
        {
            var boxFight = (BoxFight)subject;
            if (boxFight.BoxerAScore > boxFight.BoxerBScore)
                BoxerToPutMoneyOn = "Money on B more money";
            else
                BoxerToPutMoneyOn = "Money on A more money";

            Console.WriteLine("RISKYLAYER:{0}", BoxerToPutMoneyOn);
        }
    }

    class ConservativePlayer : IObserver
    {
        public string BoxerToPutMoneyOn { get; set; }
        public void Update(ISubject subject)
        {
            var boxFight = (BoxFight)subject;
            if (boxFight.BoxerAScore < boxFight.BoxerBScore)
                BoxerToPutMoneyOn = "Money on B it's safer";
            else
                BoxerToPutMoneyOn = "Money on A it's safer";

            Console.WriteLine("CONSERVATIVEPLAYER:{0}", BoxerToPutMoneyOn);
        }
    }

    public interface ISubject
    {
        void AttachObserver(IObserver observer);
        void DetachObserver(IObserver observer);
        void Notify();
    }

    public class BoxFight : ISubject
    {
        public List<IObserver> Observers { get; private set; }
        public int RoundNumber { get; private set; }
        private Random random = new Random();

        public int BoxerAScore { get; set; }
        public int BoxerBScore { get; set; }

        public BoxFight()
        {
            Observers = new();
        }

        public void AttachObserver(IObserver observer) => Observers.Add(observer);
        public void DetachObserver(IObserver observer) => Observers.Remove(observer);
        public void NextRound()
        {
            RoundNumber++;
            BoxerAScore += random.Next(0, 5);
            BoxerBScore += random.Next(0, 5);

            Notify();
        }
        public void Notify()
        {
            foreach (var observer in Observers)
                observer.Update(this);
        }
    }

    public class Client
    {
        public void Main()
        {
            var boxFight = new BoxFight();

            var riskyPlayer = new RiskyPlayer();
            var conservativePlayer = new ConservativePlayer();

            boxFight.AttachObserver(riskyPlayer);
            boxFight.AttachObserver(conservativePlayer);

            boxFight.NextRound();
            boxFight.NextRound();
            boxFight.NextRound();
            boxFight.NextRound();
        }
    }
}