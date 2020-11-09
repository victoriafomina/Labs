using System;

namespace Laba3_Newtons_Method
{
    public class MyFunction : IFunction
    {
        public double Value(double x)
        {
            return Math.Sqrt(1 + x * x) + x;
        }

        public void Print()
        {
            Console.WriteLine("sqrt(1 + x * x) + x");
        }
    }
}