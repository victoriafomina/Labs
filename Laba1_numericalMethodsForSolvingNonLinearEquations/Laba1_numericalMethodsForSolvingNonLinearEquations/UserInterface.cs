using System;
using System.Collections.Generic;

namespace Laba1_numericalMethodsForSolvingNonLinearEquations
{

    public class UserInterface
    {
        private Dictionary<int, (double, double)> signChangeIntervals = new Dictionary<int, (double, double)>();

        public void Run()
        {
            IFunction function = new MyFunction();
            Console.WriteLine("ЧИСЛЕННЫЕ МЕТОДЫ РЕШЕНИЯ НЕЛИНЕЙНЫХ УРАВНЕНИЙ\n");
            Console.WriteLine(function.ToString());

            const double leftBorder = -1;
            const double rightBorder = 3;
            Console.WriteLine($"[{leftBorder}, {rightBorder}]");

            double accuracy = Math.Pow(10, -8);
            Console.WriteLine($"eps = {accuracy}\n");

            const int numberOfParts = 1000;
            double step = Math.Abs(rightBorder - leftBorder) / numberOfParts;

            Dictionary<int, string> solvingMethods = new Dictionary<int, string>(4);
            solvingMethods.Add(0, "Метод половинного деления");
            solvingMethods.Add(1, "Метод Ньютона (метод касательных)");
            solvingMethods.Add(2, "Модифицированный метод Ньютона");
            solvingMethods.Add(3, "Метод секущих");

            SolvingMethod solver;
            SeparationOfRoots(leftBorder, rightBorder, numberOfParts, function);

            while (true)
            {
                PrintInfoAboutMethods();
                int key = Convert.ToInt32(Console.ReadLine());

                switch (key)
                {
                    case 0:
                        for (var i = 0; i < signChangeIntervals.Count; ++i)
                        {
                            solver = new BisectionMethod(solvingMethods[key], function, signChangeIntervals[i].Item1,
                                    signChangeIntervals[i].Item2, step, accuracy);
                            solver.PrintResults();
                        }                       
                        break;
                    case 1:
                        for (var i = 0; i < signChangeIntervals.Count; ++i)
                        {
                            solver = new NewtonsMethod(solvingMethods[key], function, signChangeIntervals[i].Item1,
                                    signChangeIntervals[i].Item2, step, accuracy);
                            solver.PrintResults();
                        }
                        break;
                    case 2:
                        for (var i = 0; i < signChangeIntervals.Count; ++i)
                        {
                            solver = new ModifiedNewtonsMethod(solvingMethods[key], function, signChangeIntervals[i].Item1,
                                    signChangeIntervals[i].Item2, step, accuracy);
                            solver.PrintResults();
                        }
                        break;
                    case 3:
                        for (var i = 0; i < signChangeIntervals.Count; ++i)
                        {
                            solver = new SecantMethod(solvingMethods[key], function, signChangeIntervals[i].Item1,
                                    signChangeIntervals[i].Item2, step, accuracy);
                            solver.PrintResults();
                        }
                        break;
                    case 4:
                        return;
                }
            }

        }

        private static void PrintInfoAboutMethods()
        {
            Console.WriteLine("Введите целое число");
            Console.WriteLine("0 - метод половинного деления");
            Console.WriteLine("1 - метод Ньютона (метод касательных)");
            Console.WriteLine("2 - модифицированный метод Ньютона");
            Console.WriteLine("3 - метод секущих");
            Console.WriteLine("4 - выход\n");
        }

        private void SeparationOfRoots(double leftBorder, double rightBorder, int numberOfParts, IFunction function)
        {
            double step = (leftBorder + rightBorder) / numberOfParts;
            double currentLeftPoint = leftBorder;
            double currentRightPoint = leftBorder + step;
            double currentLeftPointValue = function.FunctionValue(currentLeftPoint);
            int countSignChangeIntervals = 0;

            while (currentRightPoint <= rightBorder)
            {
                double currentRightPointValue = function.FunctionValue(currentRightPoint);

                if (currentLeftPointValue * currentRightPointValue <= 0)
                {
                    Console.WriteLine($"[{currentLeftPoint}, {currentRightPoint}]");

                    signChangeIntervals.Add(countSignChangeIntervals, (currentLeftPoint, currentRightPoint));
                    ++countSignChangeIntervals;
                }

                currentLeftPoint = currentRightPoint;
                currentRightPoint += step;
                currentLeftPointValue = currentRightPointValue;
            }

            Console.WriteLine($"Количество отрезков смены знака функции: {countSignChangeIntervals}\n");
        }
    }
}
