using System;
using System.Collections.Generic;
using System.Text;

namespace Laba6_CauchyProblem
{
    public class UserInterface
    {
        private List<(double, double)> exactSolutions;
        private double step = 0.1;
        private int N = 10;
        private int xZero = 0;
        private int yZero = 1;
        private List<(double, double)> taylorsValues;
        private IFunction function;
        private CauchyProblemLogic logic;

        public UserInterface(IFunction function)
        {
            this.function = function;
            exactSolutions = new List<(double, double)>();
            taylorsValues = new List<(double, double)>();
            logic = new CauchyProblemLogic(xZero, yZero, function);
        }

        public void Run()
        {
            Console.WriteLine("Численное решение Задачи Коши для обыкновенного дифференциального уравнения первого порядка\n");
            PrintExactSolutions();
            PrintTaylor();
        }

        private void CalculateExactSolutions()
        {
            exactSolutions.Clear();

            for (var i = -2; i <= N; ++i)
            {
                exactSolutions.Add((xZero + i * step, function.Value(xZero + i * step)));
            }
        }

        private void PrintExactSolutions()
        {
            CalculateExactSolutions();

            Console.WriteLine("Точное решение задачи Коши");
            Console.WriteLine("  x  |  f(x)");
            
            for (var i = 0; i < exactSolutions.Count; ++i)
            {
                Console.WriteLine($"{exactSolutions[i].Item1} | {exactSolutions[i].Item2}");
            }
            Console.WriteLine();
        }

        private void PrintTaylor()
        {
            taylorsValues.Clear();
            int count = 0;
            Console.WriteLine("Решение задачи Коши методом разложения в ряд Тейлора");
            Console.WriteLine(" x |    f(x)    |  абсолютная погрешность ");
            
            for (var i = -2; i < N + 1; ++i)
            {
                double currentPoint = xZero + i * step;
                double taylorValue = logic.TaylorPolynomialValue(currentPoint);
                Console.WriteLine($"{currentPoint} | {taylorValue} | {Math.Abs(exactSolutions[i + 2].Item2) - taylorValue}");

                if (count < 5)
                {
                    taylorsValues.Add((currentPoint, taylorValue));
                    ++count;
                }    
            }
            Console.WriteLine();
        }
    }
}
