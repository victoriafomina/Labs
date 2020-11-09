﻿using System;
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
        int numberOfParts;
        QuadratureFormulaLeftRectangles leftRectangles;
        QuadratureFormulaRightRectangles rightRectangles;
        QuadratureFormulaMediumRectangles mediumRectangles;
        QuadratureFormulaSimpson simpson;
        QuadratureFormulaTrapezoid trapezoid;

        public UserInterface(IFunction weightFunction, IFunction function)
        {
            this.weightFunction = weightFunction;
            this.function = function;

            leftRectangles = new QuadratureFormulaLeftRectangles(weightFunction, function);
            rightRectangles = new QuadratureFormulaRightRectangles(weightFunction, function);
            mediumRectangles = new QuadratureFormulaMediumRectangles(weightFunction, function);
            simpson = new QuadratureFormulaSimpson(weightFunction, function);
            trapezoid = new QuadratureFormulaTrapezoid(weightFunction, function);
        }

        public void Run()
        {
            FunctionInfo();

            SetBorders();
            SetNumberOfParts();

            while (true)
            {
                FunctionCalculatesInfo();
            }
        }

        private void FunctionCalculatesInfo()
        {
            Console.WriteLine($"{leftRectangles.FormulaName()}: {leftRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine($"{rightRectangles.FormulaName()}: {rightRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine($"{mediumRectangles.FormulaName()}: {mediumRectangles.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine($"{trapezoid.FormulaName()}: {trapezoid.Calculate(leftBorder, rightBorder, numberOfParts)}");
            Console.WriteLine($"{simpson.FormulaName()}: {simpson.Calculate(leftBorder, rightBorder, numberOfParts)}");
        }

        private void SetNumberOfParts()
        {
            Console.Write($"Число промежутков деления: ");
            numberOfParts = int.Parse(Console.ReadLine());
            Console.WriteLine(numberOfParts);
        }

        private void FunctionInfo()
        {
            Console.WriteLine($"Функция: {function.Print()}");
            Console.WriteLine($"Весовая функция: {weightFunction.Print()}");
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
