using System;
using System.Collections.Generic;
using System.Text;

namespace Laba5_ApproxCalcOfIntegralsQFHighestDegree
{
    public class WeightFunction : IFunction
    {
        public double Value(double x) => Math.Cos(x) * Math.Cos(x);

        public override string ToString() => "y = cos(x) * cos(x)";
    }
}
