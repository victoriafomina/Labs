using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_ReverseInterpolation
{
    /// <summary>
    /// Класс позволяет решить нелинейное уравнение метод половинного деления.
    /// </summary>
    public sealed class BisectionMethod : SolvingMethod
    {
        private double value;

        public BisectionMethod(IFunction function, double value, double leftBorder, double rightBorder,
                double step, double accuracy) : base(function,
                leftBorder, rightBorder, step, accuracy)
        {
            this.value = value;
        }

        /// <summary>
        /// Метод, запускающий процедуру решения нелинейного уравнения методом половинного деления.
        /// </summary>
        protected override void Solve()
        {
            int countStepsToGetApproximateSolution = 0;
            firstApproximation = (leftBorder + rightBorder) / 2;

            do
            {
                double midpoint = (leftBorder + rightBorder) / 2;

                if ((function.Value(leftBorder) - value) * (function.Value(midpoint) - value) <= 0)
                {
                    rightBorder = midpoint;
                }
                else
                {
                    leftBorder = midpoint;
                }

                ++countStepsToGetApproximateSolution;
            }
            while (rightBorder - leftBorder > 2 * accuracy);

            isCalculated = true;
            numberOfStepsToGetResult = countStepsToGetApproximateSolution;
            approximateResult = (leftBorder + rightBorder) / 2;
            residualModule = Math.Abs(function.Value(approximateResult));
        }
    }
}
