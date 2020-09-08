using System;

namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{
    public sealed class ModifiedNewtonsMethod : SolvingMethod
    {
        public ModifiedNewtonsMethod(string methodName, IFunction function, double leftBorder, double rightBorder,
                double step, double accuracy) : base(methodName, function,
                leftBorder, rightBorder, step, accuracy)
        { }

        protected override void Solve()
        {
            double firstPoint = (leftBorder + rightBorder) / 2;
            double nthPoint = firstPoint;

            int countStepsToGetApproximateSolution = 0;

            while (Math.Abs(function.FunctionValue(nthPoint)) > accuracy)
            {
                nthPoint -= function.FunctionValue(nthPoint) / function.DerivativeValue(firstPoint);
                ++countStepsToGetApproximateSolution;
            }

            isCalculated = true;
            numberOfStepsToGetResult = countStepsToGetApproximateSolution;
            approximateResult = nthPoint;
            residualModule = Math.Abs(function.FunctionValue(approximateResult));
        }
    }
}
