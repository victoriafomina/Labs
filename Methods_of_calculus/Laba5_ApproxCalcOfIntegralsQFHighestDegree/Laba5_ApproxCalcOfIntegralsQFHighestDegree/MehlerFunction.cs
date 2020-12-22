using System;

namespace Laba5_ApproxCalcOfIntegralsQFHighestDegree
{
    public class MehlerFunction : IFunction
    {
        public double Value(double x) => Math.Cos(x);

        public override string ToString() => "y = cos(x)";
    }
}
