using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public interface IFunction
    {
        public double Value(double x);

        public string Print();
    }
}
