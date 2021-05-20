using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NumericalDifferentiation
{
    public class FirstDerivative : IFunction
    {
        public double Value(double x) => 7.5 * Math.Exp(1.5 * 5 * x);

        public override string ToString() => "f(x) = 7,5 * e^{7,5 * x}";
    }
}
