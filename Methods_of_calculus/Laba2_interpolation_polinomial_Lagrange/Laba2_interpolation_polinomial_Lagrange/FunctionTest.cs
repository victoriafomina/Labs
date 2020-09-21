using System;
using System.Collections.Generic;
using System.Text;

namespace Laba2_interpolation_polinomial_Lagrange
{
    public class FunctionTest : IFunction
    {
        public double Value(double x)
        {
            return x * x / (1 + x * x);
        }

        public void Print()
        {
            Console.WriteLine("x^2 / (1 + x^2)");
        }
    }
}
