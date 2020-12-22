using System;
using System.Collections.Generic;
using System.Text;

namespace Laba5_ApproxCalcOfIntegralsQFHighestDegree
{
    public class ApproximateCalculationLogic
    {
        private IFunction function;
        private IFunction weightFunction;
        private IFunction functionMehler;

        public ApproximateCalculationLogic(IFunction function, IFunction weightFunction, IFunction functionMehler)
        {
            this.function = function;
            this.weightFunction = weightFunction;
            this.functionMehler = functionMehler;
        }

        public double GaussCompound(int parts, double leftBorder, double rightBorder)
        {
            double result = 0;
            double step = (rightBorder - leftBorder) / parts;

            for (var i = 0; i < parts; ++i)
            {
                double firstPoint = step / 2 * (-1 / Math.Sqrt(3)) + leftBorder + i * step + step / 2;
                double secondPoint = step / 2 * (1 / Math.Sqrt(3)) + leftBorder + i * step + step / 2;
                result += step / 2 * (weightFunction.Value(firstPoint) * function.Value(firstPoint) +
                        weightFunction.Value(secondPoint) * function.Value(secondPoint));
            }

            return result;
        }

        public double MehlersFormula(int numberOfNodes)
        {
            double result = 0;

            for (var i = 0; i < numberOfNodes; ++i)
            {
                result += functionMehler.Value(Math.Cos((2 * i + 1) / (2 * numberOfNodes) * Math.PI));
            }

            result *= Math.PI / numberOfNodes;

            return result;
        }
    }
}
