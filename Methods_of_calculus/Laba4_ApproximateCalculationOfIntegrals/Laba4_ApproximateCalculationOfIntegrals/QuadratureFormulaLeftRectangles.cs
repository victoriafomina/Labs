using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class QuadratureFormulaLeftRectangles : IApproximateCalculate
    {
        private IFunction function;

        public QuadratureFormulaLeftRectangles(IFunction function)
        {
            this.function = function;
        }

        public double Value { get; set; }

        public double Calculate(double leftBorder, double rightBorder, int numberOfParts)
        {

            double step = (rightBorder - leftBorder) / numberOfParts;

            double result = 0;

            for (var i = 0; i < numberOfParts; ++i)
            {
                result += function.Value(leftBorder + i * step);
            }

            result *= step;
            Value = result;

            return result;
        }

        public string FormulaName()
        {
            return "Составная квадратурная формула левых прямоугольников";
        }
    }
}
