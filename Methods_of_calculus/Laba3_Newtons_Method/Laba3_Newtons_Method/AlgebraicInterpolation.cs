using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba3_Newtons_Method
{
    public class AlgebraicInterpolation
    {
        private List<(double, double)> interpolationNodes;

        private List<List<((double, double), double)>> dividedDifferences;

        private double interpolationPoint;

        private int degreeOfPolynomial;

        private IFunction function;

        private double newtonPolynomialValue;

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

            dividedDifferences = new List<List<((double, double), double)>>();

            for (var i = 0; i < degreeOfPolynomial; ++i)
            {
                dividedDifferences.Add(new List<((double, double), double)>());
            }

            CalculateDividedDifferences();

            PrintSortedTable();
        }

        public double Run()
        {
            return NewtonPolynomialValue();
        }
        
        private double NewtonPolynomialValue()
        {
            double result = interpolationNodes[0].Item2;
            double bracketsProduct = 1;

            for (var i = 0; i < dividedDifferences.Count; ++i)
            {
                bracketsProduct *= interpolationPoint - interpolationNodes[i].Item1;
                result += dividedDifferences[i][0].Item2 * bracketsProduct;
            }

            newtonPolynomialValue = result;
            isCalculated = true;

            return result;
        }

        private void CalculateDividedDifferences()
        {
            for (var i = 0; i < dividedDifferences.Count; ++i)
            {
                if (i == 0)
                {
                    for (var j = 0; j < degreeOfPolynomial; ++j)
                    {
                        dividedDifferences[i].Add(((interpolationNodes[j].Item1, interpolationNodes[j + 1].Item1), (interpolationNodes[j + 1].Item2 - interpolationNodes[j].Item2) /
                                (interpolationNodes[j + 1].Item1 - interpolationNodes[j].Item1)));
                    }
                }
                else
                {
                    // Надо же не по всем узлам строить, а по degreeOfPolinomial + 1 подходящему
                    for (var j = 0; j < degreeOfPolynomial - i; ++j)
                    {
                        dividedDifferences[i].Add((((dividedDifferences[i - 1])[j].Item1.Item1, (dividedDifferences[i - 1])[j + 1].Item1.Item2), 
                                (dividedDifferences[i - 1][j + 1].Item2 - dividedDifferences[i - 1][j].Item2) / (dividedDifferences[i - 1][j + 1].Item1.Item2 - dividedDifferences[i - 1][j].Item1.Item1)));
                    }
                }
            }

            // Prints divided differences
            for (var i = 0; i < dividedDifferences.Count; ++i)
            {
                for (var j = 0; j < dividedDifferences[i].Count; ++j)
                {
                    Console.WriteLine($"({dividedDifferences[i][j].Item1.Item1}, {dividedDifferences[i][j].Item1.Item2}), {dividedDifferences[i][j].Item2}");
                }
                Console.WriteLine();
            }            
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

            return Math.Abs(newtonPolynomialValue - function.Value(interpolationPoint));
        }

        private void Sort()
        {
            interpolationNodes.Sort(((double, double) point1, (double, double) point2) =>
                    Math.Abs(point1.Item1 - interpolationPoint).CompareTo(Math.Abs(point2.Item1 - interpolationPoint)));
        }
    }
}
