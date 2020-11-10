using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class AntiderivativeFunction : IFunction
    {
        public string Print()
        {
            return "f(x) = sin(x) * cos(x) + e^x";
        }

        public double Value(double x)
        {
            // return Math.Sin(x) * Math.Cos(x) + Math.Exp(x);
            return x * x;
            // return x * x * x;
            // return x * x * x * x;
        }
    }
}
