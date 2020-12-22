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
                double approximateValue = logic.GaussCompound(parts, leftBorder, rightBorder);
                Console.WriteLine($"\nПриближенное значение интеграла: {approximateValue}\n");
                double exactValue = antiderivative.Value(rightBorder) - antiderivative.Value(leftBorder);
                Console.WriteLine($"Точное значение интеграла: {exactValue}\n");
                Console.WriteLine($"Абсолютная погрешность: {Math.Abs(exactValue - approximateValue)}");

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
                        ChangeBorders();
                        break;
                }

            }

        }

        private void ChangeBorders()
        {

        }
    }
}
