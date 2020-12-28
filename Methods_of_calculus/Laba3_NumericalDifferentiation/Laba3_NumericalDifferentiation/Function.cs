using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NumericalDifferentiation
{
    public class Function : IFunction
    {
        public double Value(double x) => Math.Exp(1.5 * 5 * x);

        public override string ToString() => "f(x) = e^{7,5 * x}";
    }
}
