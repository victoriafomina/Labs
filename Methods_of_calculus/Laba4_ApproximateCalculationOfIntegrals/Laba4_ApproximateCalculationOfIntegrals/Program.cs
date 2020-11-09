using System;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UserInterface ui = new UserInterface(new WeightFunction(), new MyFunction());
            ui.Run();
        }
    }
}
