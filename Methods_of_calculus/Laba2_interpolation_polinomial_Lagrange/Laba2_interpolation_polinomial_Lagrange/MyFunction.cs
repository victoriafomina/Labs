using System;

namespace Laba2_interpolation_polinomial_Lagrange
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
