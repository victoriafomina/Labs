using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_ApproximateCalculationOfIntegrals
{
    public class UserInterface
    {
        IFunction function;
        IFunction antiderivativeFunction;
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
            this.antiderivativeFunction = derivativeFunction;

            leftRectangles = new QuadratureFormulaLeftRectangles(function);
            rightRectangles = new QuadratureFormulaRightRectangles(function);
            mediumRectangles = new QuadratureFormulaMediumRectangles(function);
            simpson = new QuadratureFormulaSimpson(function);
            trapezoid = new QuadratureFormulaTrapezoid(function);
        }

        public void Run()
        {
            Console.WriteLine("ПРИБЛИЖЁННОЕ ВЫЧИСЛЕНИЕ ИНТЕГРАЛА ПО СОСТАВНЫМ КВАДРАТУРНЫМ ФОРМУЛАМ\n");

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
            Console.WriteLine(Math.Abs(function.Value - PreciselyValue()));
        }


        private void TheoreticalError(IApproximateCalculate function)
        {
            double result = rightBorder - leftBorder;

            double h = (rightBorder - leftBorder) / numberOfParts;

            double constant;
            if (function is QuadratureFormulaLeftRectangles || function is QuadratureFormulaRightRectangles)
            {
                constant = 1.0 / 2.0;
                result *= constant * h;
            }
            else if (function is QuadratureFormulaMediumRectangles)
            {
                constant = 1.0 / 24.0;
                result *= constant * h * h;
            }
            else if (function is QuadratureFormulaTrapezoid)
            {
                constant = 1.0 / 12.0;
                result *= constant * h * h;
            }
            else
            {
                constant = 1.0 / 2880.0;
                result *= constant * Math.Pow(h, 4);
            }

            Console.WriteLine($"Теоретическая погрешность: {result * (1 + Math.Exp(rightBorder))}\n");
        }

        private void FunctionCalculatesInfo()
        {
            Console.WriteLine($"{leftRectangles.FormulaName()}: {leftRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(leftRectangles);
            TheoreticalError(leftRectangles);

            Console.WriteLine($"{rightRectangles.FormulaName()}: {rightRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(rightRectangles);
            TheoreticalError(rightRectangles);

            Console.WriteLine($"{mediumRectangles.FormulaName()}: {mediumRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(mediumRectangles);
            TheoreticalError(mediumRectangles);

            Console.WriteLine($"{trapezoid.FormulaName()}: {trapezoid.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(trapezoid);
            TheoreticalError(trapezoid);

            Console.WriteLine($"{simpson.FormulaName()}: {simpson.Calculate(leftBorder, rightBorder, numberOfParts)}");
            AbsoluteError(simpson);
            TheoreticalError(simpson);

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
            return antiderivativeFunction.Value(rightBorder) - antiderivativeFunction.Value(leftBorder);
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
