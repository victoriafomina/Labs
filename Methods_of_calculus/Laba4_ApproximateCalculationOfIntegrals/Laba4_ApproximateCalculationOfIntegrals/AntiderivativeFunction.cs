using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class AntiderivativeFunction : IFunction
    {
        public string Print()
        {
            return "f(x) = - 4 * sin(x) * cos(x) + e^x";
        }

        public double Value(double x)
        {
            return -(4 * Math.Sin(x) * Math.Cos(x)) + Math.Exp(x);
        }
    }
}
