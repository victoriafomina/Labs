using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class UserInterface
    {
        IFunction function;
        IFunction derivativeFunction;
        double leftBorder;
        double rightBorder;
        int numberOfParts;
        QuadratureFormulaLeftRectangles leftRectangles;
        QuadratureFormulaRightRectangles rightRectangles;
        QuadratureFormulaMediumRectangles mediumRectangles;
        QuadratureFormulaSimpson simpson;
        QuadratureFormulaTrapezoid trapezoid;

        public UserInterface(IFunction function, IFunction derivativeFunction)
        {
            this.function = function;
            this.derivativeFunction = derivativeFunction;

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
                Console.WriteLine();
                PreciselyValueInfo();
                FunctionCalculatesInfo();

                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("0 - завершить программу");
                Console.WriteLine("1 - изменить пределы интегрирования");
                Console.WriteLine("2 - изменить число промежутков деления");
                Console.WriteLine("3 - изменить пределы интегрирования и число промежутков деления");
                Console.WriteLine("------------------------------------------------------------------\n");

                Console.Write("Введите число от 0 до 3: ");
                int number = int.Parse(Console.ReadLine());

                while (number < 0 || number > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nВведено некорректное значение!!!");
                    Console.ResetColor();
                    Console.Write("Введите число от 0 до 3: ");
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

        private void AbsoluteError(IApproximateCalculate function)
        {
            Console.Write("Абсолютная фактическая погрешность: ");
            Console.WriteLine(Math.Abs(function.Calculate(leftBorder, rightBorder, numberOfParts) -
                    PreciselyValue()));
            Console.WriteLine();
        }

        private void FunctionCalculatesInfo()
        {
            Console.WriteLine($"{leftRectangles.FormulaName()}: {leftRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(leftRectangles);

            Console.WriteLine($"{rightRectangles.FormulaName()}: {rightRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(rightRectangles);

            Console.WriteLine($"{mediumRectangles.FormulaName()}: {mediumRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(mediumRectangles);

            Console.WriteLine($"{trapezoid.FormulaName()}: {trapezoid.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(trapezoid);

            Console.WriteLine($"{simpson.FormulaName()}: {simpson.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(simpson);

            Console.WriteLine();
        }

        private void SetNumberOfParts()
        {
            Console.Write("Число промежутков деления: ");
            numberOfParts = int.Parse(Console.ReadLine());

            while (numberOfParts <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nЧисло промежутков деления должно быть положительным!!!");
                Console.ResetColor();
                Console.Write("Число промежутков деления: ");
                numberOfParts = int.Parse(Console.ReadLine());
            }
        }

        private void FunctionInfo()
        {
            Console.WriteLine($"Функция: {function.Print()}");
            Console.WriteLine();
        }

        private double PreciselyValue()
        {
            return derivativeFunction.Value(rightBorder) - derivativeFunction.Value(leftBorder);
        }

        private void PreciselyValueInfo()
        {
            Console.WriteLine($"Точное значение: {PreciselyValue()}\n");
        }

        private void SetBorders()
        {
            Console.WriteLine("\nПределы интегрирования");
            Console.Write("Левый предел: ");
            leftBorder = double.Parse(Console.ReadLine());
            Console.Write("Правый предел: ");
            rightBorder = double.Parse(Console.ReadLine());
            
            while (rightBorder <= leftBorder)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nПравый предел интегрерирования должен быть больше левого!");
                Console.ResetColor();
                Console.Write("Правый предел: ");
                rightBorder = double.Parse(Console.ReadLine());
            }
            Console.WriteLine();
        }
    }
}
