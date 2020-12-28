using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_ReverseInterpolation
{
    public class ReverseInterpolationLogic
    {
        private List<(double, double)> nodes;
        private List<(double, double)> interpolationNodes;
        private IFunction function;
        private InterpolarFunction interpolarFunction;
        private double value;
        private double firstMethodResult;
        private List<double> secondMethodResult;
        private Dictionary<int, (double, double)> signChangeIntervals;
        private BisectionMethod bisection;

        public ReverseInterpolationLogic(IFunction function)
        {
            signChangeIntervals = new Dictionary<int, (double, double)>();
            interpolationNodes = new List<(double, double)>();
            secondMethodResult = new List<double>();
            nodes = new List<(double, double)>();
            this.function = function;
        }

        public double ReverseFunctionMethod(double value, double leftBorder, double rightBorder, int parts, int degreeOfPolynomial)
        {
            CreateTable(leftBorder, rightBorder, parts);
            this.value = value;
            var interpolation = new AlgebraicInterpolation(nodes, value, degreeOfPolynomial, new MyFunction());
            interpolation.PrintSortedTable();
            firstMethodResult = interpolation.Run();

            return firstMethodResult;
        }

        public List<double> ReverseAlgebraicInterpolationMethod(double value, double leftBorder, double rightBorder, int parts,
                int degreeOfPolynomial, double accuracy)
        {
            CreateInterpolationTable(leftBorder, rightBorder, parts);
            this.value = value;            
            interpolarFunction = new InterpolarFunction(value);
            interpolarFunction.SetFunction(function);
            interpolarFunction.SetDegreeOfPolynomial(degreeOfPolynomial);
            interpolarFunction.SetInterpolationNodes(interpolationNodes);
            SeparationOfRoots(leftBorder, rightBorder, parts);

            for (var i = 0; i < signChangeIntervals.Count; ++i)
            {
                bisection = new BisectionMethod(interpolarFunction, signChangeIntervals[i].Item1, signChangeIntervals[i].Item2, accuracy);
                secondMethodResult.Add(bisection.ApproximateResult);
            }

            return secondMethodResult;
        }

        public void PrintTable(double leftBorder, double rightBorder, double parts)
        {
            CreateTable(leftBorder, rightBorder, parts);
            Console.WriteLine("\n  x  |  f(x)");
            Console.WriteLine("-------------");

            for (var i = 0; i < nodes.Count; ++i)
            {
                Console.WriteLine($"{i + 1}) {nodes[i].Item2} | {nodes[i].Item1}");
            }
            Console.WriteLine();
        }

        public double ReverseInterpolationDeviation() => Math.Abs(function.Value(firstMethodResult) - value);

        /// <summary>
        /// Метод разделения корней уравнения.
        /// </summary>
        private void SeparationOfRoots(double leftBorder, double rightBorder, int numberOfParts)
        {
            signChangeIntervals.Clear();

            double step = (leftBorder + rightBorder) / numberOfParts;
            double currentLeftPoint = leftBorder;
            double currentRightPoint = leftBorder + step;
            double currentLeftPointValue = interpolarFunction.Value(currentLeftPoint);
            int countSignChangeIntervals = 0;
            Console.ForegroundColor = ConsoleColor.Cyan;

            while (currentRightPoint <= rightBorder)
            {
                double currentRightPointValue = interpolarFunction.Value(currentRightPoint);

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

        private void CreateInterpolationTable(double leftBorder, double rightBorder, double numberOfParts)
        {
            interpolationNodes.Clear();
            double step = (rightBorder - leftBorder) / numberOfParts;

            for (var i = 0; i < numberOfParts + 1; ++i)
            {
                double point = leftBorder + i * step;
                interpolationNodes.Add((point, function.Value(point)));
            }
        }
    }
}
