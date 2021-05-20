using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NumericalDifferentiation
{
    public class SecondDerivative : IFunction
    {
        public double Value(double x) => 56.25 * Math.Exp(1.5 * 5 * x);

        public override string ToString() => "f(x) = 56,25 * e^{7,5 * x}";
    }
}
