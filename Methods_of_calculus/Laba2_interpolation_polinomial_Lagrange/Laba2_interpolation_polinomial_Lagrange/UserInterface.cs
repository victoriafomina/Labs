using System;
using System.Collections.Generic;
using System.Text;

namespace Laba2_interpolation_polinomial_Lagrange
{
    public static class UserInterface
    {
        public static void Run()
        {
            Console.WriteLine("ЗАДАЧА АЛГЕБРАИЧЕСКОГО ИНТЕРПОЛИРОВАНИЯ");
            Console.WriteLine("Вариант 14\n");
            const int numberOfValues = 16;
            Console.WriteLine($"Число значений в таблице: {numberOfValues}\n");
            const double leftBorder = 0;
            const double rightBorder = 1;

            IFunction function = new FunctionTest();
            List<(double, double)> interpolationNodes = new List<(double, double)>();

            Console.Write($"Введите точку интерполирования (из отрезка [{leftBorder}, {rightBorder}]): ");
            double interpolationPoint = double.Parse(Console.ReadLine());
            while (interpolationPoint < leftBorder || interpolationPoint > rightBorder)
            {
                Console.WriteLine($"\nТочка \"x = {interpolationPoint}\" не принадлежит отрезку [{leftBorder}, {rightBorder}].");
                Console.Write($"Введите точку интерполирования (из отрезка [{leftBorder}, {rightBorder}]): ");
                interpolationPoint = double.Parse(Console.ReadLine());
            }

            Console.Write($"\nВведите степень многочлена n (n < {numberOfValues}): ");
            int degreeOfPolynomial = int.Parse(Console.ReadLine());
            Console.WriteLine();

            for (var i = 0; i < numberOfValues; ++i)
            {
                double currentX = leftBorder + i * (rightBorder - leftBorder) /
                        (numberOfValues - 1);
                double valueX = function.Value(currentX);
                interpolationNodes.Add((currentX, valueX));
            }

            Console.WriteLine("Исходная таблица значений функции:");
            Utils.PrintTable(interpolationNodes, degreeOfPolynomial);

            AlgebraicInterpolation interpolation = new AlgebraicInterpolation(interpolationNodes, interpolationPoint,
                    degreeOfPolynomial, function);

            Console.WriteLine("\nЗначение интерполяционного многочлена, найденное при помощи " +
                    $"представления в форме Лагранжа: {interpolation.Run()}");
            Console.WriteLine($"\nЗначение абсолютной фактической погрешности для формы Лагранжа: {interpolation.MeasurementError()}");
        }
    }
}
