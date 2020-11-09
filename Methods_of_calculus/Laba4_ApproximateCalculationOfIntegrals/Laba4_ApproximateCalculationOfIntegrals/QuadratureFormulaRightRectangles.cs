using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class QuadratureFormulaRightRectangles : IApproximateCalculate
    {
        public double Calculate(IFunction weightFunction, IFunction function, double leftBorder, 
                double rightBorder, int numberOfParts)
        {
            double step = (rightBorder - leftBorder) / numberOfParts;

            double result = 0;

            for (var i = 0; i < numberOfParts; ++i)
            {
                result += weightFunction.Value((leftBorder + step) * i * step) *
                        function.Value((leftBorder + step) * i * step);
            }

            result *= step;

            return result;
        }

        public string FormulaName()
        {
            return "Составная квадратурная формула правых треугольников";
        }
    }
}
