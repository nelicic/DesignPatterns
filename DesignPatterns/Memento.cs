namespace DesignPatterns.Memento
{
    class Originator
    {
        private string _state;

        public Originator(string state)
        {
            _state = state;
            Console.WriteLine("Originator: My initial state is: " + state);
        }

        public void DoSomething()
        {
            Console.WriteLine("Originator: I'm doing something important.");
            _state = GenerateRandomString(30);
            Console.WriteLine($"Originator: and my state has changed to: {_state}");
        }

        private string GenerateRandomString(int length = 10)
        {
            string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];

                Thread.Sleep(12);

                length--;
            }

            return result;
        }

        public IMemento Save()
        {
            return new ConcreteMemento(_state);
        }

        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            _state = memento.GetState();
            Console.Write($"Originator: My state has changed to: {_state}");
        }
    }

    public interface IMemento
    {
        string GetName();

        string GetState();

        DateTime GetDate();
    }

    class ConcreteMemento : IMemento
    {
        private string _state;

        private DateTime _date;

        public ConcreteMemento(string state)
        {
            _state = state;
            _date = DateTime.Now;
        }

        public string GetState()
        {
            return _state;
        }

        public string GetName()
        {
            return $"{_date} / ({_state.Substring(0, 9)})...";
        }

        public DateTime GetDate()
        {
            return _date;
        }
    }
    class Caretaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private Originator _originator = null;

        public Caretaker(Originator originator)
        {
            _originator = originator;
        }

        public void Backup()
        {
            Console.WriteLine("\nCaretaker: Saving Originator's state...");
            _mementos.Add(_originator.Save());
        }

        public void Undo()
        {
            if (_mementos.Count == 0)
            {
                return;
            }

            var memento = _mementos.Last();
            _mementos.Remove(memento);

            Console.WriteLine("Caretaker: Restoring state to: " + memento.GetName());

            try
            {
                _originator.Restore(memento);
            }
            catch (Exception)
            {
                Undo();
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("Caretaker: Here's the list of mementos:");

            foreach (var memento in _mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }

    public class Client
    {
        public void Main()
        {
            Originator originator = new Originator("Super-duper-super-puper-super.");
            Caretaker caretaker = new Caretaker(originator);

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            Console.WriteLine();
            caretaker.ShowHistory();

            Console.WriteLine("\nClient: Now, let's rollback!\n");
            caretaker.Undo();

            Console.WriteLine("\n\nClient: Once more!\n");
            caretaker.Undo();

            Console.WriteLine();
        }
    }
}

namespace DesignPatterns.MementoV2
{ 
    public class GameOriginator
    {
        private GameState _state = new GameState(100, 0);

        public void Play()
        {
            Console.WriteLine(_state.ToString());
            _state = new GameState((int)(_state.Health * 0.9), _state.KilledMonsters + 2);
        }

        public GameMemento GameSave()
        {
            return new GameMemento(_state);
        }

        public void LoadGame(GameMemento memento)
        {
            _state = memento.GetState();
        }
    }

    public class GameState
    {
        public int Health { get; }
        public int KilledMonsters { get; }
        public GameState(int health, int kills)
        {
            Health = health;
            KilledMonsters = kills;
        }

        public override string ToString()
        {
            return $"Health: {Health}\nKilled Monsters: {KilledMonsters}";
        }
    }

    public class Client
    {
        public void Main()
        {
            var caretaker = new Caretaker();
            caretaker.F5();
            caretaker.ShootThatDumbAss();
            caretaker.F5();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.F9();
            caretaker.ShootThatDumbAss();
        }
    }

    public class GameMemento
    {
        private readonly GameState _state;

        public GameMemento(GameState state)
        {
            _state = state;
        }

        public GameState GetState()
        {
            return _state;
        }
    }

    public class Caretaker
    {
        private readonly GameOriginator _game = new GameOriginator();
        private readonly Stack<GameMemento> _quickSaves = new Stack<GameMemento>();

        public void ShootThatDumbAss()
        {
            _game.Play();
        }

        public void F5()
        {
            _quickSaves.Push(_game.GameSave());
        }

        public void F9()
        {
            _game.LoadGame(_quickSaves.Peek());
        }
    }
}
