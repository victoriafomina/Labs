using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class QuadratureFormulaTrapezoid : IApproximateCalculate
    {
        private IFunction function;
        private IFunction weightFunction;

        public QuadratureFormulaTrapezoid(IFunction weightFunction, IFunction function)
        {
            this.weightFunction = weightFunction;
            this.function = function;
        }

        public double Calculate(double leftBorder, double rightBorder, int numberOfParts)
        {
            double step = (rightBorder - leftBorder) / (2 * numberOfParts);

            double result = 0;

            for (var i = 0; i < numberOfParts + 1; ++i)
            {
                double temp = weightFunction.Value(leftBorder + i * step) *
                        function.Value(leftBorder + i * step);

                if (i == 0 || i == numberOfParts)
                {
                    result += temp;
                }
                else
                {
                    result += 2 * temp;
                }
            }

            result *= (rightBorder - leftBorder) / (2 * numberOfParts);

            return result;
        }

        public string FormulaName()
        {
            return "Составная квадратурная формула трапеций";
        }
    }
}
