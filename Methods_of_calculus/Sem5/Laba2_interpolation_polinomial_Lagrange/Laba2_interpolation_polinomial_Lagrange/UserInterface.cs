using System;
using System.Collections.Generic;

namespace Laba2_interpolation_polinomial_Lagrange
{
    public static class UserInterface
    {
        private static double leftBorder = 0;

        private static double rightBorder = 1;

        private static int numberOfValues = 16;

        private static List<(double, double)> interpolationNodes = new List<(double, double)>();

        private static IFunction function = new MyFunction();



        public static void Run()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("ЗАДАЧА АЛГЕБРАИЧЕСКОГО ИНТЕРПОЛИРОВАНИЯ");
            Console.WriteLine("Вариант 14\n");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Число значений в таблице: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{numberOfValues}\n");

            Console.WriteLine($"[{leftBorder}, {rightBorder}]\n");

            while (true)
            {
                SetInterpolationNodes();
                double interpolationPoint = InterpolationPointInput();

                Console.Write($"\nВведите степень многочлена n (n < {numberOfValues}): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                int degreeOfPolynomial = int.Parse(Console.ReadLine());

                while (degreeOfPolynomial >= numberOfValues || degreeOfPolynomial < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введено некорректное n");
                    Console.ResetColor();
                    Console.Write($"\nВведите степень многочлена n (n < {numberOfValues}): ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    degreeOfPolynomial = int.Parse(Console.ReadLine());
                    Console.ResetColor();
                }
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
                Console.WriteLine("\n\n------------------------------------------");
                Console.ResetColor();
                Console.WriteLine("0 - выход");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("------------------------------------------");
                Console.ResetColor();
                Console.WriteLine("1 - ввести новые значения x и n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("------------------------------------------");
                Console.ResetColor();
                Console.WriteLine("2 - изменить число значений в таблице");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("------------------------------------------");
                Console.ResetColor();
                Console.WriteLine("3 - изменить границы отрезка");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("------------------------------------------");
                Console.ResetColor();
                Console.WriteLine("4 - изменить границы отрезка и число значений в таблице");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("------------------------------------------\n\n");
                Console.ResetColor();
                int number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 0: 
                        return;
                    case 1:
                        break;
                    case 2:
                        SetNumberOfValuesInTable();
                        break;
                    case 3:
                        SetBorders();
                        break;
                    case 4:                        
                        SetBorders();
                        SetNumberOfValuesInTable();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Было введено некорректное значение!");
                        Console.ResetColor();
                        break;
                }                
            }
        }

        private static void SetNumberOfValuesInTable()
        {
            Console.Write("\nЗадайте количество значений в таблице: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            numberOfValues = int.Parse(Console.ReadLine());
            Console.ResetColor();

            while (numberOfValues < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nКоличество значений в таблице не должно быть меньше 2\n");
                Console.ResetColor();
                Console.Write("Задайте количество значений в таблице: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                numberOfValues = int.Parse(Console.ReadLine());
            }
            Console.ResetColor();
        }

        private static void SetBorders()
        {
            Console.Write("\nВведите левую границу отрезка: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            leftBorder = double.Parse(Console.ReadLine());
            Console.ResetColor();

            Console.Write("\nВведите правую границу отрезка: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            rightBorder = double.Parse(Console.ReadLine());
            while (rightBorder <= leftBorder)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nЗначение правой границы отрезка должно быть больше значения левой границы!\n");
                Console.ResetColor();
                Console.Write("\nВведите правую границу отрезка: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                rightBorder = int.Parse(Console.ReadLine());
            }
            Console.WriteLine($"\n[{leftBorder}, {rightBorder}]\n");
            Console.ResetColor();
        }

        private static void SetInterpolationNodes()
        {
            interpolationNodes.Clear();
            for (var i = 0; i < numberOfValues; ++i)
            {
                double currentX = leftBorder + i * (rightBorder - leftBorder) /
                        (numberOfValues - 1);
                double valueX = function.Value(currentX);
                interpolationNodes.Add((currentX, valueX));
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nИсходная таблица значений функции:");
            Utils.PrintTable(interpolationNodes, numberOfValues - 1);
        }

        private static double InterpolationPointInput()
        {
            Console.Write($"\nВведите точку интерполирования (предпочтительно из отрезка [{leftBorder}, {rightBorder}]): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            double interpolationPoint = double.Parse(Console.ReadLine());
            Console.ResetColor();

            return interpolationPoint;
        }
    }
}
