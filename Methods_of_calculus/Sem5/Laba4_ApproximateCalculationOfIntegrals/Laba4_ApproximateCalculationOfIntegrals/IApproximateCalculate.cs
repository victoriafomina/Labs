using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public interface IApproximateCalculate
    {
        public double Calculate(double leftBorder, double rightBorder, int numberOfParts);

        public double Value { get; set; }

        public string FormulaName();
    }
}
