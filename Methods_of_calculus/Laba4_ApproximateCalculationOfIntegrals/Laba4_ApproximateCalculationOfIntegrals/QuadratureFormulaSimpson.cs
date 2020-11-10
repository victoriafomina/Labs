using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class QuadratureFormulaSimpson : IApproximateCalculate
    {
        private IFunction function;

        public QuadratureFormulaSimpson(IFunction function)
        {
            this.function = function;
        }

        public double Value { get; private set; }

        public double Calculate(double leftBorder, double rightBorder, int numberOfParts)
        {
            double step = (rightBorder - leftBorder) / (2 * numberOfParts);

            double result = 0;

            for (var i = 0; i < 2 * numberOfParts + 1; ++i)
            {
                double temp = function.Value(leftBorder + i * step);

                if (i == 0 || i == 2 * numberOfParts)
                {
                    result += temp;
                }
                else if (i % 2 != 0)
                {
                    result += 4 * temp;
                }
                else
                {
                    result += 2 * temp;
                }
            }

            result *= (rightBorder - leftBorder) / (6 * numberOfParts);

            Value = result;

            return result;
        }

        public string FormulaName()
        {
            return "Составная квадратурная формула Симпсона";
        }
    }
}
