using System;

namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{
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

            protected set { }
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

            protected set { } 
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

            Console.WriteLine(methodName);
            Console.WriteLine($"Приближенное решение уравнения: {ApproximateResult}");
            Console.WriteLine($"Количество шагов, потребовавшееся для получения приближенного решения уравнения" +
                    $" с заданной точностью: {NumberOfStepsToGetResult}");
            Console.WriteLine($"Модуль невязки: {ResidualModule}");

            if (this is BisectionMethod)
            {
                Console.WriteLine($"Длина последнего отрезка: {rightBorder - leftBorder}");
            }

            Console.WriteLine();
        }
    }
}
