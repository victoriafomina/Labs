using System;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UserInterface ui = new UserInterface(new MyFunction(), new DerivativeFunction());
            ui.Run();
        }
    }
}
