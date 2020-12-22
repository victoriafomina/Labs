using System;
using System.Collections.Generic;
using System.Text;

namespace Laba5_ApproxCalcOfIntegralsQFHighestDegree
{
    public class MehlerAntiderivativeFunction : IFunction
    {
        public double Value(double x)
        {
            return -Math.Sqrt(1 - x * x);
        }

        public override string ToString()
        {
            return "f(x) = -sqrt(1 - x * x)";
        }
    }
}
