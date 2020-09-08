using System;

namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{
    /// <summary>
    /// Этот класс реализует интерфейс IFunction.
    /// </summary>
    public class MyFunction : IFunction
    {
        /// <summary>
        /// Вычисляет значение функции в точке х.
        /// </summary>
        public double FunctionValue(double x)
        {
            return (x - 1) * (x - 1) - Math.Exp(-x);
        }

        /// <summary>
        /// Вычисляет значение производной функции в точке х.
        /// </summary>
        public double DerivativeValue(double x)
        {
            return 2 * (x - 1) + Math.Exp(-x);
        }

        public override string ToString()
        {
            return "f(x) = (x - 1)^2 - e^{-x}";
        }
    }
}
