using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NewtonsMethod
{
    public class UserInterface
    {
        private double leftBorder = 0;

        private double rightBorder = 1;

        private int numberOfNodes = 16;

        private int degreeOfPolynomial;

        private double interpolationPoint;

        private List<(double, double)> interpolationNodes = new List<(double, double)>();

        private IFunction function = new Function();

        private AlgebraicInterpolation logic;

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("ЗАДАЧА АЛГЕБРАИЧЕСКОГО ИНТЕРПОЛИРОВАНИЯ (МЕТОД НЬЮТОНА)");
            Console.WriteLine("Вариант 14\n");            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{function}\n");
            Console.Write("Число значений в таблице: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{numberOfNodes}\n");
            Console.WriteLine($"[{leftBorder}, {rightBorder}]\n");
            SetInterpolationNodes();
            PrintAllNodes();

            Menu();            
        }

        private void Menu()
        {
            while (true)
            {
                InterpolationPointInput();
                SetDegreeOfPolynomial();

                logic = new AlgebraicInterpolation(interpolationNodes, interpolationPoint, degreeOfPolynomial, function);
                logic.PrintNodesForInterpolation();
                var result = logic.Value();
                Console.WriteLine($"Значение интерполяционного многочлена, найденное при помощи представления в форме Ньютона: {result}");
                Console.WriteLine($"Значение абсолютной фактической погрешности для формы Ньютона: {Math.Abs(result - function.Value(interpolationPoint))}");

                PrintMenuInfo();

                int number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 0:
                        return;
                    case 1:
                        break;
                    case 2:
                        SetNumberOfNodes();
                        SetInterpolationNodes();
                        PrintAllNodes();
                        break;
                    case 3:
                        SetBorders();
                        break;
                    case 4:
                        SetBorders();
                        SetNumberOfNodes();
                        SetInterpolationNodes();
                        PrintAllNodes();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Было введено некорректное значение!");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void SetBorders()
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

        private void PrintMenuInfo()
        {
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
        }

        private void SetDegreeOfPolynomial()
        {
            Console.Write($"\nВведите степень многочлена (n), которым будем интерполировать функцию (n < {numberOfNodes}): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            degreeOfPolynomial = int.Parse(Console.ReadLine());

            while (degreeOfPolynomial >= numberOfNodes || degreeOfPolynomial < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Введено некорректное n");
                Console.ResetColor();
                Console.Write($"\nВведите степень многочлена n (n < {numberOfNodes} and n > 1): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                degreeOfPolynomial = int.Parse(Console.ReadLine());
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        private void PrintAllNodes()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nИсходная таблица значений функции:");

            for (var i = 0; i < numberOfNodes; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i + 1}) ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"x = {interpolationNodes[i].Item1} | f(x) = {interpolationNodes[i].Item2}");
            }

            Console.ResetColor();
        }

        private void SetInterpolationNodes()
        {
            interpolationNodes.Clear();

            for (var i = 0; i < numberOfNodes; ++i)
            {
                double currentX = leftBorder + i * (rightBorder - leftBorder) / (numberOfNodes - 1);
                double valueOfX = function.Value(currentX);
                interpolationNodes.Add((currentX, valueOfX));
            }
        }

        private void InterpolationPointInput()
        {
            Console.Write($"\nВведите точку интерполирования (предпочтительно из отрезка [{leftBorder}, {rightBorder}]): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            interpolationPoint = double.Parse(Console.ReadLine());
            Console.ResetColor();
        }

        private void SetNumberOfNodes()
        {
            Console.Write("\nЗадайте количество значений в таблице: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            numberOfNodes = int.Parse(Console.ReadLine());
            Console.ResetColor();

            while (numberOfNodes < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nКоличество значений в таблице не должно быть меньше 2\n");
                Console.ResetColor();
                Console.Write("Задайте количество значений в таблице: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                numberOfNodes = int.Parse(Console.ReadLine());
            }

            Console.ResetColor();
        }
    }
}
