using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba3_Newtons_Method
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

            dividedDifferences = new List<List<double>>();

            for (var i = 0; i < interpolationNodes.Count - 1; ++i)
            {
                dividedDifferences.Add(new List<double>());
            }

            CalculateDividedDifferences();

            PrintSortedTable();
        }

        public double Run()
        {
            return -1; //NewtonPolynomialValue();
        }
        /*
        private double NewtonPolynomialValue()
        {
            double result = interpolationNodes[0].Item2;

            for (int i = 0; i < )
        }
        */

        public void CalculateDividedDifferences()
        {
            for (var i = 0; i < dividedDifferences.Count; ++i)
            {
                for (var j = 0; j < dividedDifferences.Count - i; ++j)
                {
                    if (dividedDifferences[j].Count == 0)
                    {
                        dividedDifferences[j].Add((interpolationNodes[i + 1].Item2 - interpolationNodes[i].Item2) /
                                (interpolationNodes[i + 1].Item1 - interpolationNodes[i].Item1));
                    }
                    else if (j + 1 < dividedDifferences.Count)
                    {
                        dividedDifferences[j].Add((dividedDifferences[j + 1][dividedDifferences.Count - 1] - dividedDifferences[j][dividedDifferences.Count - 1]) /
                                (interpolationNodes[j + i + 1].Item1 - interpolationNodes[i].Item1));
                    }
                }
            }

            for (var i = 0; i < dividedDifferences.Count; ++i)
            {
                for (var j = 0; j < dividedDifferences.Count - i - 1; ++j)
                {
                    Console.Write($"{dividedDifferences[i][j]},  ");
                }
                Console.WriteLine();
            }
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
