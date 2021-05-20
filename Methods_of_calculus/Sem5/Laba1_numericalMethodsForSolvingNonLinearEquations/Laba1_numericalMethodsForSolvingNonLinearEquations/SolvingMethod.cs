using System;

namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{
    /// <summary>
    /// Абстрактный класс определяет поведение классов-методов решения уравнений.
    /// </summary>
    public abstract class SolvingMethod
    {
        protected string methodName;
        protected IFunction function;
        protected double leftBorder;
        protected double rightBorder;
        protected double step;
        protected bool isCalculated = false;
        protected double accuracy;
        protected int numberOfStepsToGetResult;
        protected double residualModule;
        protected double approximateResult;
        protected double firstApproximation;

        public SolvingMethod(string methodName, IFunction function, double leftBorder, double rightBorder, double step, 
                double accuracy)
        {
            this.methodName = methodName;
            this.function = function;
            this.leftBorder = leftBorder;
            this.rightBorder = rightBorder;
            this.step = step;
            this.accuracy = accuracy;
        }

        /// <returns>Значение приближенного решения уравнения.</returns>
        public double ApproximateResult 
        { 
            get
            {
                if (!isCalculated)
                {
                    Solve();
                    isCalculated = true;
                }

                return approximateResult;
            }
        }

        /// <returns>
        /// Количество шагов, потребовавшееся для нахождения приближенного решения с
        /// заданной точностью.
        /// </returns>
        public int NumberOfStepsToGetResult 
        {
            get
            {
                if (!isCalculated)
                {
                    Solve();
                    isCalculated = true;
                }

                return numberOfStepsToGetResult;
            }
        }

        /// <returns>Модуль невязки.</returns>
        public double ResidualModule 
        { 
            get
            {
                if (!isCalculated)
                {
                    Solve();
                    isCalculated = true;
                }

                return residualModule;
            }

            protected set { } 
        }

        /// <summary>
        /// Находит приближенные решения уравнения с помощью метода.
        /// </summary>
        protected abstract void Solve();

        /// <summary>
        /// Выводит на консоль информацию по решению уравнения.
        /// </summary>
        public void PrintResults()
        {
            if (!isCalculated)
            {
                Solve();
                isCalculated = true;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(methodName);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Начальное приближение к корню: {firstApproximation}");
            Console.WriteLine($"Приближенное решение уравнения: {ApproximateResult}");
            Console.WriteLine($"Количество шагов, потребовавшееся для получения приближенного решения уравнения" +
                    $" с заданной точностью: {NumberOfStepsToGetResult}");
            Console.WriteLine($"Модуль невязки: {ResidualModule}");

            if (this is BisectionMethod)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Длина последнего отрезка: {rightBorder - leftBorder}");
            }

            Console.WriteLine();
        }
    }
}
