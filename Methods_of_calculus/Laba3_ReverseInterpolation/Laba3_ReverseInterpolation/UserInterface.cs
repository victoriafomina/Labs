using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_ReverseInterpolation
{
    public class UserInterface
    {
        private double leftBorder;
        private double rightBorder;
        private IFunction function = new MyFunction();
        private ReverseInterpolationLogic logic = new ReverseInterpolationLogic(new MyFunction());

        public void Run()
        {
            Console.WriteLine("Лабораторная работа №3");
            Console.WriteLine("Задание №3.1\nЗадача обратного интерполирования\n");
            Console.WriteLine($"Функция: {function}");
            SetBorders();

            while (true)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1 способ решения (через обратную функцию и алгебраическую интерполяцию)");
                Console.ResetColor();
                Console.Write("\nРешаем f(x) = y\nВведите y: ");
                double value = double.Parse(Console.ReadLine());
                Console.Write("Введите количество точек разбиения отрезка: ");
                int parts = int.Parse(Console.ReadLine());
                Console.Write("Введите полиномом какой степени будем интерполировать (> 0 и  < кол-ва точек разбиения): ");
                int degreeOfPolynomial = int.Parse(Console.ReadLine());
                while (degreeOfPolynomial >= parts || degreeOfPolynomial <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Введите полиномом какой степени будем интерполировать (> 0 и  < кол-ва точек разбиения): ");
                    degreeOfPolynomial = int.Parse(Console.ReadLine());
                    Console.ResetColor();
                }
                Console.Write($"\nПриближенное решение уравнения: {logic.ReverseFunctionMethod(value, leftBorder, rightBorder, parts - 1, degreeOfPolynomial)}");
                Console.Write($"\nМодуль невязки: {logic.ReverseInterpolationDeviation()}");
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("2 способ решения (через алгебраическую интерполяцию )\n");
                Console.ResetColor();
                Console.Write("Введите порядок погрешности (целое натуральное число): ");
                int order = int.Parse(Console.ReadLine());
                double accuracy = 1 / Math.Pow(10, -order);
                Console.Write($"\nПриближенное решение уравнения: {logic.SolveUsingBisection(value, leftBorder, rightBorder, parts, degreeOfPolynomial, accuracy)}");
                Console.ResetColor();
                Console.WriteLine("\n-------------------------------------");
                Console.WriteLine("0 - выход");
                Console.WriteLine("1 - запустить заново");
                Console.WriteLine("2 - изменить границы отрезка");
                int number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 0:
                        return;
                    case 1:
                        break;
                    case 2:
                        SetBorders();
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

            Console.Write("Введите правую границу отрезка: ");
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
    }
}
