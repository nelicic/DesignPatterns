namespace DesignPatterns.TemplateMethod
{
    abstract class AbstractClass
    {
        public void TemplateMethod()
        {
            BaseOperation1();
            RequiredOperations1();
            BaseOperation2();
            Hook1();
            RequiredOperation2();
            BaseOperation3();
            Hook2();
        }

        protected void BaseOperation1()
        {
            Console.WriteLine("AbstractClass says: I am doing the bulk of the work");
        }

        protected void BaseOperation2()
        {
            Console.WriteLine("AbstractClass says: But I let subclasses override some operations");
        }

        protected void BaseOperation3()
        {
            Console.WriteLine("AbstractClass says: But I am doing the bulk of the work anyway");
        }

        protected abstract void RequiredOperations1();

        protected abstract void RequiredOperation2();

        protected virtual void Hook1() { }

        protected virtual void Hook2() { }
    }

    class ConcreteClass1 : AbstractClass
    {
        protected override void RequiredOperations1()
        {
            Console.WriteLine("ConcreteClass1 says: Implemented Operation1");
        }

        protected override void RequiredOperation2()
        {
            Console.WriteLine("ConcreteClass1 says: Implemented Operation2");
        }
    }
    class ConcreteClass2 : AbstractClass
    {
        protected override void RequiredOperations1()
        {
            Console.WriteLine("ConcreteClass2 says: Implemented Operation1");
        }

        protected override void RequiredOperation2()
        {
            Console.WriteLine("ConcreteClass2 says: Implemented Operation2");
        }

        protected override void Hook1()
        {
            Console.WriteLine("ConcreteClass2 says: Overridden Hook1");
        }
    }

    class Client
    {
        public void Main()
        {
            AbstractClass abstractClass;

            Console.WriteLine("Same client code can work with different subclasses:");
            abstractClass = new ConcreteClass1();
            abstractClass.TemplateMethod();

            Console.Write("\n");

            Console.WriteLine("Same client code can work with different subclasses:");
            abstractClass = new ConcreteClass2();
            abstractClass.TemplateMethod();
        }
    }

}


namespace DesignPatterns.TemplateMethodV2
{
    public class MessageSearcher
    {
        protected DateTime DateSent;
        protected string PersonName;
        protected int ImportanceLevel;

        public MessageSearcher(DateTime dateSent, string personName, int importanceLevel)
        {
            DateSent = dateSent;
            PersonName = personName;
            ImportanceLevel = importanceLevel;
        }

        protected virtual void CreateDateCriteria()
        {
            Console.WriteLine("Standard date criteria has been applied.");
        }
        protected virtual void CreateSentPersonCriteria()
        {
            Console.WriteLine("Standard person criteria has been applied.");
        }
        protected virtual void CreateImportanceCriteria()
        {
            Console.WriteLine("Standard importance criteria has been applied.");
        }

        public string Search()
        {
            CreateDateCriteria();
            CreateSentPersonCriteria();
            Console.WriteLine("Template method does some verification accordingly to search algo.");
            CreateImportanceCriteria();
            Console.WriteLine("Template method verifies if message could be so important or useless from person provided in criteria.");
            Console.WriteLine();
            return "Some list of messages...";
        }
    }

    public class ImportantMessagesSearcher : MessageSearcher
    {
        public ImportantMessagesSearcher(DateTime dateSent, String personName)
            : base(dateSent, personName, 3)
        { }
        protected override void CreateImportanceCriteria()
            => Console.WriteLine("Special importance criteria has been formed: IMPORTANT");
    }

    class UselessMessagesSearcher : MessageSearcher
    {
        public UselessMessagesSearcher(DateTime dateSent, String personName)
        : base(dateSent, personName, 1)
        { }
        protected override void CreateImportanceCriteria()
            => Console.WriteLine("Special importance criteria has been formed: USELESS");
    }

    public class Client
    {
        public void Main()
        {
            MessageSearcher searcher = new UselessMessagesSearcher(DateTime.Today, "Sally");
            searcher.Search();

            searcher = new ImportantMessagesSearcher(DateTime.Today, "Killer");
            searcher.Search();
        }
    }
}