using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class DerivativeFunction
    {
        public double Value(double x)
        {
            return -(Math.Sin(x) * Math.Cos(x)) + Math.Exp(x);
        }
    }
}
