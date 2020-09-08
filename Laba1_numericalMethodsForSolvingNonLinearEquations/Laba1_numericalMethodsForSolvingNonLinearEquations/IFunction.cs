namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{
    public interface IFunction
    {
        /// <summary>
        /// Вычисляет значение функции в точке х.
        /// </summary>
        public double FunctionValue(double x);

        /// <summary>
        /// Вычисляет значение производной функции в точке х.
        /// </summary>
        public double DerivativeValue(double x);
    }
}
