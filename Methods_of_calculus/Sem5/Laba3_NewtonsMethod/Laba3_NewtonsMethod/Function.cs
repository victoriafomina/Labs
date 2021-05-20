using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NewtonsMethod
{
    public class Function : IFunction
    {
        public double Value(double x) => Math.Sqrt(1 + x * x) + x;

        public override string ToString() => "f(x) = sqrt(1 + x * x) + x";
    }
}
