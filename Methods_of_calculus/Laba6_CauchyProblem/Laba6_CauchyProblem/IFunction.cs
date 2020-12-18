using System;
using System.Collections.Generic;
using System.Text;

namespace Laba6_CauchyProblem
{
    public interface IFunction
    {
        public double Value(double x);

        public double DerivativeValue(int derivativeOrder, double x, double y);
    }
}
