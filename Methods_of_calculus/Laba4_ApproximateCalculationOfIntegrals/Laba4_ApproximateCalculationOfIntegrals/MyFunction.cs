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
        }

        public string Print()
        {
            return "f(x) = cos(2x) + e^x";
        }
    }
}
