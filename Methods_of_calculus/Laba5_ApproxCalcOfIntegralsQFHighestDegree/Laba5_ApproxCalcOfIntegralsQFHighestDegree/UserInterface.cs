using System;
using System.Collections.Generic;
using System.Text;

namespace Laba5_ApproxCalcOfIntegralsQFHighestDegree
{
    public class UserInterface
    {
        private double leftBorder = 0;
        private double rightBorder = 1;
        private ApproximateCalculationLogic logic = new ApproximateCalculationLogic(new Function(), new WeightFunction(), new MehlerFunction());
        private IFunction antiderivative = new Antiderivative();

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Приближённое вычисление интегралов при помощи квадратурных формул Наивысшей Алгебраической Степени Точности " +
                    "(КФ НАСТ)\n\n");
            Console.ResetColor();

            while (true)
            {
                Console.WriteLine("Вычислим при помощи составной КФ Гаусса с 2-мя узлами:\n");
                Console.WriteLine($"Функция: {new Function()}");
                Console.WriteLine($"Весовая функция: {new WeightFunction()}");
                Console.WriteLine($"Отрезок: [{leftBorder}; {rightBorder}]\n");
                Console.Write("Введите число промежутков деления: ");
                int parts = int.Parse(Console.ReadLine());
                double approximateValueGaussCompound = logic.GaussCompound(parts, leftBorder, rightBorder);
                Console.WriteLine($"\nПриближенное значение интеграла: {approximateValueGaussCompound}\n");
                double exactValueGaussCompound = antiderivative.Value(rightBorder) - antiderivative.Value(leftBorder);
                Console.WriteLine($"Точное значение интеграла: {exactValueGaussCompound}\n");
                Console.WriteLine($"Абсолютная погрешность: {Math.Abs(exactValueGaussCompound - approximateValueGaussCompound)}");

                Console.WriteLine("--------------------------------------------------------");

                Console.WriteLine("Вычислим при помощи КФ типа Гаусса с 2-мя узлами:\n");
                Console.WriteLine($"Функция: {new Function()}");
                Console.WriteLine($"Весовая функция: {new WeightFunction()}");
                Console.WriteLine($"Отрезок: [{leftBorder}; {rightBorder}]\n");
                double approximateValueQFTypeGauss = logic.GaussianQuadratureFormula(leftBorder, rightBorder);
                Console.WriteLine($"\nПриближенное значение интеграла: {approximateValueQFTypeGauss}\n");
                double exactValueQFTypeGauss = antiderivative.Value(rightBorder) - antiderivative.Value(leftBorder);
                logic.PrintMoments();
                Console.WriteLine();
                logic.PrintQuadratureFormulasCoefficients();
                Console.WriteLine($"\nТочное значение интеграла: {exactValueQFTypeGauss}\n");
                Console.WriteLine($"Абсолютная погрешность: {Math.Abs(exactValueQFTypeGauss - approximateValueQFTypeGauss)}");

                Console.WriteLine("--------------------------------------------------------");

                Console.WriteLine("Вычислим при помощи КФ Мелера:\n");
                Console.WriteLine($"Функция: {new MehlerFunction()}");
                Console.WriteLine($"Весовая функция: 1 / sqrt(1 - x * x)");
                Console.WriteLine($"Отрезок: [-1; 1]\n");
                Console.Write("Введите число узлов: ");
                int numberOfNodes = int.Parse(Console.ReadLine());
                Console.WriteLine($"\nПриближенное значение интеграла: {logic.MehlersFormula(numberOfNodes)}");

                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("0 - выйти");
                Console.WriteLine("1 - запустить заново");
                Console.WriteLine("2 - изменить границы отрезка для формул Гаусса");
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
            leftBorder = int.Parse(Console.ReadLine());
            Console.ResetColor();

            Console.Write("\nВведите правую границу отрезка: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            rightBorder = int.Parse(Console.ReadLine());
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
