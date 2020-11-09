using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class UserInterface
    {
        IFunction function;
        double leftBorder;
        double rightBorder;
        int numberOfParts;
        QuadratureFormulaLeftRectangles leftRectangles;
        QuadratureFormulaRightRectangles rightRectangles;
        QuadratureFormulaMediumRectangles mediumRectangles;
        QuadratureFormulaSimpson simpson;
        QuadratureFormulaTrapezoid trapezoid;

        public UserInterface(IFunction function)
        {
            this.function = function;

            leftRectangles = new QuadratureFormulaLeftRectangles(function);
            rightRectangles = new QuadratureFormulaRightRectangles(function);
            mediumRectangles = new QuadratureFormulaMediumRectangles(function);
            simpson = new QuadratureFormulaSimpson(function);
            trapezoid = new QuadratureFormulaTrapezoid(function);
        }

        public void Run()
        {
            FunctionInfo();

            SetBorders();
            SetNumberOfParts();

            while (true)
            {
                FunctionCalculatesInfo();

                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("0 - завершить программу");
                Console.WriteLine("1 - изменить пределы интегрирования");
                Console.WriteLine("2 - изменить число промежутков деления");
                Console.WriteLine("3 - изменить пределы интегрирования и число промежутков деления");
                Console.WriteLine("------------------------------------------------------------------\n");

                int number = int.Parse(Console.ReadLine());
                while (number < 0 || number > 3)
                {
                    Console.WriteLine("Введено некорректное значение!!!");
                    number = int.Parse(Console.ReadLine());
                }

                switch (number)
                {
                    case 0:
                        return;
                    case 1:
                        SetBorders();
                        break;
                    case 2:
                        SetNumberOfParts();
                        break;
                    case 3:
                        SetBorders();
                        SetNumberOfParts();
                        break;
                }
            }
        }

        private void FunctionCalculatesInfo()
        {
            Console.WriteLine($"{leftRectangles.FormulaName()}: {leftRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine($"{rightRectangles.FormulaName()}: {rightRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine($"{mediumRectangles.FormulaName()}: {mediumRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine($"{trapezoid.FormulaName()}: {trapezoid.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine($"{simpson.FormulaName()}: {simpson.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine();
        }

        private void SetNumberOfParts()
        {
            Console.Write($"Число промежутков деления: ");
            numberOfParts = int.Parse(Console.ReadLine());
            Console.WriteLine();
        }

        private void FunctionInfo()
        {
            Console.WriteLine($"Функция: {function.Print()}");
            Console.WriteLine();
        }

        private void SetBorders()
        {
            Console.WriteLine("Пределы интегрирования");
            Console.Write("Левый предел: ");
            leftBorder = double.Parse(Console.ReadLine());
            Console.Write("Правый предел: ");
            rightBorder = double.Parse(Console.ReadLine());
            Console.WriteLine();
        }
    }
}
