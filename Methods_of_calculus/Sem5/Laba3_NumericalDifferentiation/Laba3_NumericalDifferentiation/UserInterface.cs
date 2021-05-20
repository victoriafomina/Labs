using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NumericalDifferentiation
{
    public class UserInterface
    {
        private NumericalDifferentiationLogic logic;
        private double step;
        private double leftBorder;
        private int numberOfPoints;

        public UserInterface()
        {
            logic = new NumericalDifferentiationLogic(new Function(), new FirstDerivative(), new SecondDerivative());
        }

        public void Run()
        {
            Console.WriteLine("Задание 3.2");
            Console.WriteLine("Нахождение производных таблично-заданной функции по формулам численного дифференцирования");            
            Console.WriteLine($"Функция: {new Function()}\n");

            while (true)
            {
                InputLeftBorder();
                InputStep();
                InputNumberOfPoints();

                logic.CreateTable(leftBorder, step, numberOfPoints);
                logic.PrintTable();
                logic.Run();
            }
        }

        private void InputStep()
        {
            Console.Write("Введите шаг (вещ. число > 0): ");
            step = double.Parse(Console.ReadLine());
            Console.WriteLine();

            while (step <= 0)
            {
                Console.Write("Введите шаг (вещ. число > 0): ");
                step = double.Parse(Console.ReadLine());
                Console.WriteLine();
            }
        }

        private void InputLeftBorder()
        {
            Console.Write("Введите левую границу отрезка: ");
            leftBorder = double.Parse(Console.ReadLine());
            Console.WriteLine();
        }

        private void InputNumberOfPoints()
        {
            Console.Write("Введите количество узлов (>= 3): ");
            numberOfPoints = int.Parse(Console.ReadLine());
            Console.WriteLine();

            while (numberOfPoints < 3)
            {
                Console.Write("Введите количество узлов (>= 3): ");
                numberOfPoints = int.Parse(Console.ReadLine());
                Console.WriteLine();
            }
        }
    }
}
