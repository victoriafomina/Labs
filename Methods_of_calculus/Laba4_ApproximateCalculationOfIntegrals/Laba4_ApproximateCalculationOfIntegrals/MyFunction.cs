using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class MyFunction : IFunction
    {
        public double Value(double x)
        {
            return x;
        }

        public string Print()
        {
            return "y = x";
        }
    }
}
