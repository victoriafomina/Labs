using System;
using System.Collections.Generic;

namespace Laba3_ReverseInterpolation
{
    public class AlgebraicInterpolation
    {
        private List<(double, double)> interpolationNodes;

        private double interpolationPoint;

        private int degreeOfPolynomial;

        private IFunction function;

        private double lagrangePolynomialValue;

        private List<List<double>> dividedDifferences;

        private bool sorted;

        private bool isCalculated;



        public AlgebraicInterpolation(List<(double, double)> interpolationNodes, double interpolationPoint,
                int degreeOfPolynomial, IFunction function)
        {
            interpolationNodes.Sort(((double, double) point1, (double, double) point2) =>
                    point1.Item1.CompareTo(point2.Item1));

            this.interpolationNodes = interpolationNodes;
            this.interpolationPoint = interpolationPoint;
            this.degreeOfPolynomial = degreeOfPolynomial;
            this.function = function;
            sorted = false;
            isCalculated = false;

            dividedDifferences = new List<List<double>>(interpolationNodes.Count - 1);
            for (int i = 0; i < dividedDifferences.Count; ++i)
            {
                dividedDifferences[i] = new List<double>();
            }

            PrintSortedTable();
        }

        public double Run()
        {
            return LagrangePolynomialValue();
        }

        private double LagrangePolynomialValue()
        {
            lagrangePolynomialValue = 0;

            for (var i = 0; i < degreeOfPolynomial + 1; ++i)
            {
                lagrangePolynomialValue += ParenthesesProduct(i, interpolationPoint) *
                        interpolationNodes[i].Item2 / ParenthesesProduct(i, interpolationNodes[i].Item1);
            }

            isCalculated = true;

            return lagrangePolynomialValue;
        }

        private double ParenthesesProduct(int index, double point)
        {
            double result = 1;

            for (var i = 0; i < degreeOfPolynomial + 1; ++i)
            {
                if (i != index)
                {
                    result *= (point - interpolationNodes[i].Item1);
                }
            }

            return result;
        }

        public void PrintSortedTable()
        {
            if (!sorted)
            {
                Sort();
                sorted = true;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Узлы, по которым строится интерполяционный полином:");
            Utils.PrintTable(interpolationNodes, degreeOfPolynomial);
        }

        public double MeasurementError()
        {
            if (!isCalculated)
            {
                throw new InvalidOperationException("Погрешность вычислений не может быть найдена, так как вычисления" +
                        " не были произведены!!!");
            }

            return Math.Abs(lagrangePolynomialValue - function.Value(interpolationPoint));
        }

        private void Sort()
        {
            interpolationNodes.Sort(((double, double) point1, (double, double) point2) =>
                    Math.Abs(point1.Item1 - interpolationPoint).CompareTo(Math.Abs(point2.Item1 - interpolationPoint)));
        }
    }
}