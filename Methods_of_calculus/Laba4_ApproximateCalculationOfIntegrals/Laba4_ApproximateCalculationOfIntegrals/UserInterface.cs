using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class UserInterface
    {
        IFunction weightFunction;
        IFunction function;
        double leftBorder;
        double rightBorder;

        public UserInterface(IFunction weightFunction, IFunction function)
        {
            this.weightFunction = weightFunction;
            this.function = function;
        }

        public void Run()
        {
            Console.WriteLine($"Функция: {function.Print()}");
            Console.WriteLine($"Весовая функция: {weightFunction.Print()}");

            SetBorders();
            SetNumberOfParts();
        }

        private void SetNumberOfParts()
        {
            Console.Write($"Число промежутков деления: ");
            int numberOfParts = int.Parse(Console.ReadLine());
            Console.WriteLine(numberOfParts);
        }

        private void SetBorders()
        {
            Console.WriteLine("Пределы интегрирования");
            Console.Write("Левый предел: ");
            leftBorder = double.Parse(Console.ReadLine());
            Console.WriteLine(leftBorder);
            Console.Write("Правый предел:");
            rightBorder = double.Parse(Console.ReadLine());
            Console.WriteLine(rightBorder);
        }
    }
}
