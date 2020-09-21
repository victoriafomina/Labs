using System;
using System.Collections.Generic;

namespace Laba2_interpolation_polinomial_Lagrange
{
    public static class UserInterface
    {
        public static void Run()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("ЗАДАЧА АЛГЕБРАИЧЕСКОГО ИНТЕРПОЛИРОВАНИЯ");
            Console.WriteLine("Вариант 14\n");
            const int numberOfValues = 16;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Число значений в таблице: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{numberOfValues}\n");
            const double leftBorder = 0;
            const double rightBorder = 1;

            IFunction function = new FunctionTest();
            List<(double, double)> interpolationNodes = new List<(double, double)>();
            for (var i = 0; i < numberOfValues; ++i)
            {
                double currentX = leftBorder + i * (rightBorder - leftBorder) /
                        (numberOfValues - 1);
                double valueX = function.Value(currentX);
                interpolationNodes.Add((currentX, valueX));
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Исходная таблица значений функции:");
            Utils.PrintTable(interpolationNodes, numberOfValues - 1);

            while (true)
            {
                double interpolationPoint = InterpolationPointInput(leftBorder, rightBorder);

                Console.Write($"\nВведите степень многочлена n (n < {numberOfValues}): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                int degreeOfPolynomial = int.Parse(Console.ReadLine());
                Console.WriteLine();

                AlgebraicInterpolation interpolation = new AlgebraicInterpolation(interpolationNodes, interpolationPoint,
                        degreeOfPolynomial, function);

                Console.Write("\nЗначение интерполяционного многочлена, найденное при помощи " +
                        "представления в форме Лагранжа: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{interpolation.Run()}");
                Console.ResetColor();

                Console.Write("\nЗначение абсолютной фактической погрешности для формы Лагранжа: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{interpolation.MeasurementError()}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n--------------------------------");
                Console.ResetColor();
                Console.WriteLine("0 - выход\n1 - ввести новые значения x и n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("--------------------------------\n");
                Console.ResetColor();
                int number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 0: 
                        return;
                    case 1:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Было введено некорректное значение!");
                        Console.ResetColor();
                        break;
                }                
            }
        }

        private static double InterpolationPointInput(double leftBorder, double rightBorder)
        {
            Console.Write($"\nВведите точку интерполирования (из отрезка [{leftBorder}, {rightBorder}]): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            double interpolationPoint = double.Parse(Console.ReadLine());
            Console.ResetColor();

            while (interpolationPoint < leftBorder || interpolationPoint > rightBorder)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nТочка \"x = {interpolationPoint}\" не принадлежит отрезку [{leftBorder}, {rightBorder}]!");
                Console.ResetColor();
                Console.Write($"Введите точку интерполирования (из отрезка [{leftBorder}, {rightBorder}]): ");
                interpolationPoint = double.Parse(Console.ReadLine());
            }

            return interpolationPoint;
        }
    }
}
