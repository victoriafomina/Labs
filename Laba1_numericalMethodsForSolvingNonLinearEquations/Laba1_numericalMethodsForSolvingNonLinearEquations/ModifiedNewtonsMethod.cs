using System;

namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{
    /// <summary>
    /// Класс позволяет решить нелинейное уравнение модифицированным методом Ньютона.
    /// </summary>
    public sealed class ModifiedNewtonsMethod : SolvingMethod
    {
        public ModifiedNewtonsMethod(string methodName, IFunction function, double leftBorder, double rightBorder,
                double step, double accuracy) : base(methodName, function,
                leftBorder, rightBorder, step, accuracy)
        { }

        protected override void Solve()
        {
            double firstPoint = leftBorder;
            double nthPoint = firstPoint;
            firstApproximation = firstPoint;

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
