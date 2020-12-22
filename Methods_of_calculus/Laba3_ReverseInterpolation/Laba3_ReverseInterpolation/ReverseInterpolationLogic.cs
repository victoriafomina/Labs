using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_ReverseInterpolation
{
    public class ReverseInterpolationLogic
    {
        private List<(double, double)> nodes = new List<(double, double)>();
        private IFunction function;
        private double value;
        private double firstMethodResult;
        private Dictionary<int, (double, double)> signChangeIntervals = new Dictionary<int, (double, double)>();

        public ReverseInterpolationLogic(IFunction function)
        {
            this.function = function;
        }

        private void CreateTable(double leftBorder, double rightBorder, double numberOfParts)
        {
            nodes.Clear();
            double step = (rightBorder - leftBorder) / numberOfParts;

            for (var i = 0; i < numberOfParts + 1; ++i)
            {
                double point = leftBorder + i * step;
                nodes.Add((function.Value(point), point));
            }
        }

        public double ReverseFunctionMethod(double value, double leftBorder, double rightBorder, int parts, int degreeOfPolynomial)
        {
            CreateTable(leftBorder, rightBorder, parts);
            this.value = value;
            var interpolation = new AlgebraicInterpolation(nodes, value, degreeOfPolynomial, new MyFunction());

            firstMethodResult = interpolation.Run();

            return firstMethodResult;
        }

        public void PrintTable(double leftBorder, double rightBorder, double parts)
        {
            CreateTable(leftBorder, rightBorder, parts);
            Console.WriteLine("\n  x  |  f(x)");
            Console.WriteLine("-------------");

            for (var i = 0; i < nodes.Count; ++i)
            {
                Console.WriteLine($"{nodes[i].Item2} | {nodes[i].Item1}");
            }
            Console.WriteLine();
        }

        public double ReverseInterpolationDeviation() => Math.Abs(function.Value(firstMethodResult) - value);

        public double SolveUsingBisection(double value, double leftBorder, double rightBorder, int parts, int degreeOfPolynomial, double accuracy)
        {
            CreateTable(leftBorder, rightBorder, parts);
            SeparationOfRoots(leftBorder, rightBorder, parts, function);
            var bisection = new BisectionMethod(new BisectionInterpolarFunction(nodes, degreeOfPolynomial), value, leftBorder, rightBorder,
                    (rightBorder - leftBorder) / parts, accuracy);

            return bisection.ApproximateResult;
        }

        /// <summary>
        /// Метод разделения корней уравнения.
        /// </summary>
        private void SeparationOfRoots(double leftBorder, double rightBorder, int numberOfParts, IFunction function)
        {
            double step = (leftBorder + rightBorder) / numberOfParts;
            double currentLeftPoint = leftBorder;
            double currentRightPoint = leftBorder + step;
            double currentLeftPointValue = function.Value(currentLeftPoint);
            int countSignChangeIntervals = 0;
            Console.ForegroundColor = ConsoleColor.Cyan;

            while (currentRightPoint <= rightBorder)
            {
                double currentRightPointValue = function.Value(currentRightPoint);

                if (currentLeftPointValue * currentRightPointValue <= 0)
                {
                    Console.WriteLine($"[{currentLeftPoint}, {currentRightPoint}]");

                    signChangeIntervals.Add(countSignChangeIntervals, (currentLeftPoint, currentRightPoint));
                    ++countSignChangeIntervals;
                }

                currentLeftPoint = currentRightPoint;
                currentRightPoint += step;
                currentLeftPointValue = currentRightPointValue;
            }

            Console.WriteLine($"Количество отрезков смены знака функции: {countSignChangeIntervals}\n");
        }
    }
}
