using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class MyFunction : IFunction
    {
        public double Value(double x)
        {
            return Math.Cos(2 * x) + Math.Exp(x);
            // return 1;
            // return 2 * x;
            // return 3 * x * x;
            // return 4 * x * x * x;
        }

        public string Print()
        {
            // return "f(x) = cos(2x) + e^x";
            // return "f(x) = 3x^2";
            return "f(x) = 4*x^3";
        }
    }
}
