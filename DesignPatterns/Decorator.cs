using System.Text;

namespace DesignPatterns.Decorator
{
    public abstract class EditOfTextBase
    {
        private readonly EditOfTextBase _editOfTextBase;

        protected EditOfTextBase(EditOfTextBase editOfTextBase = null)
        {
            _editOfTextBase = editOfTextBase;
        }

        public virtual string GetFormattedText(string text)
        {
            if (_editOfTextBase != null)
                text = _editOfTextBase.GetFormattedText(text);

            return text;
        }
    } 

    class ToUpperText : EditOfTextBase
    {
        public ToUpperText(EditOfTextBase editOfTextBase = null) : base(editOfTextBase)
        { }

        public override string GetFormattedText(string text)
        {
            return base.GetFormattedText(text)?.ToUpper();
        }
    }

    class ReplaceSpaceWithLog : EditOfTextBase
    {
        public ReplaceSpaceWithLog(EditOfTextBase editOfTextBase = null) : base(editOfTextBase)
        { }

        public override string GetFormattedText(string text)
        {
            Console.WriteLine("All ' ' will be replaced with '+'");
            return base.GetFormattedText(text).Replace(" ", "+");
        }
    }

    class ToBase64 : EditOfTextBase
    {
        public ToBase64(EditOfTextBase editOfTextBase = null) : base(editOfTextBase)
        { }

        public override string GetFormattedText(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
    }

    class ToHtmlTemplate : EditOfTextBase
    {
        private readonly string _htmlPage = "<!DOCTYPE html>\n<html>\n<body>\n<div>{0}</div>\n</body>\n</html>";
        public ToHtmlTemplate(EditOfTextBase editOfTextBase = null) : base(editOfTextBase)
        { }
        public override string GetFormattedText(string text)
        {
            var lines = base.GetFormattedText(text).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Format(_htmlPage, string.Join("<be/>\n", lines));
        }
    }


    public class Client
    { 
        public void Main()
        {
            var text = "Hello!\n This article is about the pattern \"Decorator\"";

            var formattedText = new ToHtmlTemplate(new ToUpperText(new ReplaceSpaceWithLog()));

            Console.WriteLine(formattedText.GetFormattedText(text));
        }
    }
}


namespace DesignPatterns.DecoratorV2
{ 
    class Car
    {
        protected string BrandName { get; set; }
        public virtual void Go()
        {
            Console.WriteLine("I'm " + BrandName + "and I'm on my way...");
        }
    }

    class Mercedes : Car
    {
        public Mercedes()
        {
            BrandName = "Mercedes";
        }
    }

    class DecoratorCar : Car
    { 
        protected Car DecoratedCar { get; set; }
        public DecoratorCar(Car decoratedCar)
        {
            DecoratedCar = decoratedCar;
        }

        public override void Go()
        {
            DecoratedCar.Go();
        }
    }

    class AmbulanceCar : DecoratorCar
    {
        public AmbulanceCar(Car decoratedCar) : base(decoratedCar)
        { }

        public override void Go()
        {
            base.Go();
            Console.WriteLine("Beep beeep beep");
        }
    }

    public class Client
    {
        public void Main()
        {
            var doctorsMers = new AmbulanceCar(new Mercedes());
            doctorsMers.Go();
        }
    }
}
