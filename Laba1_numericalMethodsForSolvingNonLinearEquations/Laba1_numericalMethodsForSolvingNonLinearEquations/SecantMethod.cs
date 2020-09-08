﻿using System;

namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{
    public sealed class SecantMethod : SolvingMethod
    {
        public SecantMethod(string methodName, IFunction function, double leftBorder, double rightBorder,
                double step, double accuracy) : base(methodName, function,
                leftBorder, rightBorder, step, accuracy)
        { }

        protected override void Solve()
        {
            double previousPoint = leftBorder;
            double currentPoint = rightBorder;
            double nextPoint = 0;

            int countStepsToGetApproximateSolution = 0;

            while (Math.Abs(function.FunctionValue(currentPoint)) > accuracy)
            {
                nextPoint = currentPoint - function.FunctionValue(currentPoint) /
                        (function.FunctionValue(currentPoint) - function.FunctionValue(previousPoint)) * (currentPoint - previousPoint);
                previousPoint = currentPoint;
                currentPoint = nextPoint;
                ++countStepsToGetApproximateSolution;
            }

            isCalculated = true;
            numberOfStepsToGetResult = countStepsToGetApproximateSolution;
            approximateResult = nextPoint;
            residualModule = Math.Abs(function.FunctionValue(approximateResult));
        }
    }
}
