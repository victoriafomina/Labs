using System;
using System.Collections.Generic;

namespace Laba2_interpolation_polinomial_Lagrange
{
    public class AlgebraicInterpolation
    {
        private List<(double, double)> interpolationNodes;

        private double interpolationPoint;

        private int degreeOfPolynomial;

        private IFunction function;

        private double lagrangePolynomialValue;

        private bool sorted;

        private bool isCalculated;



        public AlgebraicInterpolation(List<(double, double)> interpolationNodes, double interpolationPoint,
                int degreeOfPolynomial, IFunction function)
        {
            this.interpolationNodes = interpolationNodes;
            this.interpolationPoint = interpolationPoint;
            this.degreeOfPolynomial = degreeOfPolynomial;
            this.function = function;
            sorted = false;
            isCalculated = false;
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
                    Math.Abs(point1.Item1 - interpolationPoint).CompareTo(Math.Abs(point2.Item2 - interpolationPoint)));
        }        
    }
}
