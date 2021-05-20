using System;
using System.Collections.Generic;
using System.Text;

namespace Laba5_ApproxCalcOfIntegralsQFHighestDegree
{
    public class Antiderivative : IFunction
    {
        public double Value(double x) => -1.0 / 3 * Math.Pow(Math.Cos(x), 3);
    }
}
