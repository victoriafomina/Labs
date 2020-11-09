using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class WeightFunction : IFunction
    {
        public double Value(double x)
        {
            return 1;
        }

        public string Print()
        {
            return "y = 1";
        }
    }
}
