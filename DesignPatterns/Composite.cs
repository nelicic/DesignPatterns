namespace DesignPatterns.Composite
{
    public abstract class Component
    {
        public Component() { }
        public abstract string Operation();
        public virtual void Add(Component component)
        {
            throw new NotImplementedException();
        }
        public virtual void Remove(Component component)
        {
            throw new NotImplementedException();
        }
        public virtual bool IsComposite()
        {
            return true;
        }
    }

    class Leaf : Component
    {
        public override string Operation()
        {
            return "Leaf";
        }

        public override bool IsComposite()
        {
            return false;
        }
    }

    class Composite : Component 
    {
        protected List<Component> _children = new();

        public override void Add(Component component)
        {
            _children.Add(component);
        }

        public override void Remove(Component component)
        {
            _children.Remove(component);
        }

        public override string Operation()
        {
            int i = 0;
            string result = "Branch(";

            foreach (Component item in _children)
            {
                result += item.Operation();
                if (i != _children.Count - 1)
                    result += "+";
                i++;
            }

            return result + ")";
        }
    }

    class Client
    {
        public void Main()
        {
            Leaf leaf = new Leaf();
            Console.WriteLine("Client: I get a simple component:");
            ClientCode(leaf);

            Composite tree = new Composite();
            Composite branch1 = new Composite();
            branch1.Add(new Leaf());
            branch1.Add(new Leaf());
            Composite branch2 = new Composite();
            branch2.Add(new Leaf());
            tree.Add(branch1);
            tree.Add(branch2);
            Console.WriteLine("Client: Now I've got a composite tree:");
            ClientCode(tree);

            Console.Write("Client: I don't need to check the components classes even when managing the tree:\n");
            ClientCode2(tree, leaf);
        }
        public void ClientCode(Component leaf)
        {
            Console.WriteLine($"RESULT: {leaf.Operation()}\n");
        }

        public void ClientCode2(Component component1, Component component2)
        {
            if (component1.IsComposite())
            {
                component1.Add(component2);
            }

            Console.WriteLine($"RESULT: {component1.Operation()}");
        }
    }

}
