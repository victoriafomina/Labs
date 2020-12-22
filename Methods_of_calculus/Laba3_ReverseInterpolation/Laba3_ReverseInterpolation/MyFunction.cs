using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_ReverseInterpolation
{
    public class MyFunction : IFunction
    {
        public double Value(double x) => Math.Exp(x) + x + 2;
    }
}
