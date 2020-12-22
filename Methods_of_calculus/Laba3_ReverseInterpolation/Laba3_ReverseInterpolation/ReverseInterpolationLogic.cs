using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_ReverseInterpolation
{
    public class ReverseInterpolationLogic
    {
        private List<(double, double)> nodes = new List<(double, double)>();
        private IFunction function;

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
            PrintTable();
            var interpolation = new AlgebraicInterpolation(nodes, value, degreeOfPolynomial, new MyFunction());

            return interpolation.Run();
        }

        private void PrintTable()
        {
            Console.WriteLine("\n  x  |  f(x)");
            Console.WriteLine("-------------");

            for (var i = 0; i < nodes.Count; ++i)
            {
                Console.WriteLine($"{nodes[i].Item1} | {nodes[i].Item2}");
            }
            Console.WriteLine();
        }
    }
}
