using System;
using System.Collections.Generic;
using System.Text;

namespace Laba5_ApproxCalcOfIntegralsQFHighestDegree
{
    public class Function : IFunction
    {
        public double Value(double x) => Math.Sin(x);

        public override string ToString() => "y = sin(x)";
    }
}
