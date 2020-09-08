﻿using System;

namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{
    public sealed class BisectionMethod : SolvingMethod
    {
        public BisectionMethod(string methodName, IFunction function, double leftBorder, double rightBorder,
                double step, double accuracy) : base(methodName, function, 
                leftBorder, rightBorder, step, accuracy) { }

        protected override void Solve()
        {
            Console.WriteLine(methodName);

            int countStepsToGetApproximateSolution = 0;

            do
            {
                double midpoint = (leftBorder + rightBorder) / 2;

                if (function.FunctionValue(leftBorder) * function.FunctionValue(midpoint) <= 0)
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
            residualModule = Math.Abs(function.FunctionValue(approximateResult));
        }
    }
}
