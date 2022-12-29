using System.Collections;

namespace DesignPatterns.Iterator
{
    public abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();
        public abstract int Key();
        public abstract object Current();
        public abstract bool MoveNext();
        public abstract void Reset();
    }

    abstract class IteratorAggregate : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }

    class AlphabeticalOrderIterator : Iterator
    {
        private WordsCollection _collection;
        private int _position = -1;

        private bool _reverse = false;

        public AlphabeticalOrderIterator(WordsCollection collection, bool reverse = false)
        {
            _collection = collection;
            _reverse = reverse;

            if (reverse)
            {
                _position = collection.getItems().Count;
            }
        }

        public override object Current()
        {
            return _collection.getItems()[_position];
        }

        public override int Key()
        {
            return _position;
        }

        public override bool MoveNext()
        {
            int updatedPosition = _position + (_reverse ? -1 : 1);

            if (updatedPosition >= 0 && updatedPosition < _collection.getItems().Count)
            {
                _position = updatedPosition;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Reset()
        {
            _position = _reverse ? _collection.getItems().Count - 1 : 0;
        }
    }

    class WordsCollection : IteratorAggregate
    {
        List<string> _collection = new List<string>();

        bool _direction = false;

        public void ReverseDirection()
        {
            _direction = !_direction;
        }

        public List<string> getItems()
        {
            return _collection;
        }

        public void AddItem(string item)
        {
            _collection.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            return new AlphabeticalOrderIterator(this, _direction);
        }
    }

    public class Client
    {
        public void Main()
        {
            var collection = new WordsCollection();
            collection.AddItem("First");
            collection.AddItem("Second");
            collection.AddItem("Third");

            Console.WriteLine("Straight traversal:");

            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("\nReverse traversal:");

            collection.ReverseDirection();

            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }
        }
    }
}


/*namespace DesignPatterns.IteratorV2
{
    public class Soldier
    {
        public string Name;
        public int Health;
        private const int SoldierHealthPoints = 100;
        protected virtual int MaxHealthPoints { get => SoldierHealthPoints; }

        public Soldier(string name)
        {
            Name = name;
        }

        public void Treat()
        {
            Health = MaxHealthPoints;
            Console.WriteLine(Name);
        }
    }

    public class Hero : Soldier
    {
        private const int HeroHealthPoints = 500;
        protected override int MaxHealthPoints { get => HeroHealthPoints; }
        public Hero(string name) : base(name)
        { }
    }

    public class SoldiersIterator
    {
        private readonly Army _army;
        private bool _heroIsIterated;
        private int _currentGroup;
        private int _currentGroupSoldier;

        public SoldiersIterator(Army army)
        {
            _army = army;
            _heroIsIterated = false;
            _currentGroup = 0;
            _currentGroupSoldier = 0;
        }

        public bool HasNext()
        {
            if (!_heroIsIterated)
                return true;
            if (_currentGroup < _army.ArmyGroups.Count)
                return true;
            if (_currentGroup == _army.ArmyGroups.Count - 1)
                if (_currentGroupSoldier < _army.ArmyGroup[_currentGroup].Soldiers.Count)
                    return true;
            return false;
        }

        public Soldier Next()
        {
            Soldier nextSoldier;
            if (_currentGroup < _army.ArmyGroups.Count)
            {
                if (_currentGroupSoldier < _army.ArmyGroups[_currentGroup].Soldiers.Count)
                {
                    nextSoldier = _army.ArmyGroups[_currentGroup].Soldiers[_currentGroupSoldier];
                    _currentGroupSoldier++;
                }
                else 
                {
                    _currentGroup++;
                    _currentGroupSoldier = 0;
                    return Next();
                }
            }
            else if (!_heroIsIterated)
            {
                _heroIsIterated = true;
                return _army.ArmyHero;
            }    
            else
            {
                throw new Exception("End of collection");
            }   
            return nextSoldier;
        }
    }
}*/