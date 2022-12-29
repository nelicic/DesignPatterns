namespace DesignPatterns.Command
{
    public class Command
    { }

    public interface ICommand
    {
        void Execute();
    }

    public class SimpleCommand : ICommand
    {
        private string _payload = string.Empty;
        public SimpleCommand(string payload)
        {
            _payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See, I can do simple things like printing ({_payload})");
        }
    }

    public class ComplexCommand : ICommand
    {
        private Receiver _receiver;

        private string _a;
        private string _b;

        public ComplexCommand(Receiver receiver, string a, string b)
        {
            _receiver = receiver;
            _a = a;
            _b = b;
        }

        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
            _receiver.DoSomething(_a);
            _receiver.DoSomethingElse(_b);
        }
    }

    public class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Working on ({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on ({b}.)");
        }
    }

    class Invoker
    {
        private ICommand _onStart;

        private ICommand _onFinish;

        public void SetOnStart(ICommand command)
        {
            _onStart = command;
        }

        public void SetOnFinish(ICommand command)
        {
            _onFinish = command;
        }

        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want something done before I begin?");
            if (_onStart is ICommand)
            {
                _onStart.Execute();
            }

            Console.WriteLine("Invoker: ...doing something really important...");

            Console.WriteLine("Invoker: Does anybody want something done after I finish?");
            if (_onFinish is ICommand)
            {
                _onFinish.Execute();
            }
        }
    }

    public class Client
    {
        public void Main()
        {
            Invoker invoker = new Invoker();
            invoker.SetOnStart(new SimpleCommand("Say Hi!"));
            Receiver receiver = new Receiver();
            invoker.SetOnFinish(new ComplexCommand(receiver, "Send email", "Save report"));

            invoker.DoSomethingImportant();
        }
    }
}

namespace DesignPatterns.CommandV2
{
    public interface ICommand
    {
        void Execute();
    }

    class Team
    {
        public string Name { get; set; }
        public Team(string name)
        {
            Name = name;
        }

        public void CompleteProject(List<Requirement> requirements)
        {
            string str = string.Empty;
            requirements.ForEach(requirement => str += requirement.Name + " ");
            Console.WriteLine($"Team completed the project with those requirements {str}");
        }
    }

    class Requirement
    {
        public string Name { get; set; }
        public Requirement(string name)
        {
            Name = name;
        }
    }

    class YouAsProjectManagerCommand : ICommand
    {
        protected Team Team { get; set; }
        protected List<Requirement> Requirements { get; set; }
        public YouAsProjectManagerCommand(Team team, List<Requirement> requirements)
        {
            Team = team;
            Requirements = requirements;
        }
        public void Execute()
        {
            Team.CompleteProject(Requirements);
        }
    }

    class HeroDeveloperCommand : ICommand
    {
        protected HeroDeveloper HeroDeveloper { get; set; }
        public string ProjectName { get; set; }
        public HeroDeveloperCommand(HeroDeveloper herodDeveloper, string projectName)
        {
            HeroDeveloper = herodDeveloper;
            ProjectName = projectName;
        }

        public void Execute()
        {
            HeroDeveloper.DoAllHardWork(ProjectName);
        }
    }

    class HeroDeveloper
    { 
        public void DoAllHardWork(string projectName)
        {
            Console.WriteLine("Hero developer finished the project");
        }
    }


    class Customer
    {
        protected List<ICommand> Commands { get; set; }
        public Customer()
        {
            Commands = new List<ICommand>();
        }
        public void AddCommand(ICommand command)
        {
            Commands.Add(command);
        }
        public void SignContractWithBoss()
        {
            foreach (var command in Commands)
                command.Execute();
        }
    }

    public class Client
    {
        public void Main()
        {
            var customer = new Customer();

            var team = new Team("My team");

            var requirements = new List<Requirement>()
            {
                new Requirement("req 1"),
                new Requirement("req 2"),
                new Requirement("req 3")
            };

            ICommand commandX = new YouAsProjectManagerCommand(team, requirements);

            var heroDeveloper = new HeroDeveloper();

            ICommand commandA = new HeroDeveloperCommand(heroDeveloper, "ProjectX");

            customer.AddCommand(commandX);
            customer.AddCommand(commandA);

            customer.SignContractWithBoss();
        }
    }
}