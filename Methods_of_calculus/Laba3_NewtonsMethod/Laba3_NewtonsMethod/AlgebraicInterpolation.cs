using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NewtonsMethod
{
    public class AlgebraicInterpolation
    {
        private IFunction function;
        private List<(double, double)> interpolationNodes;
        private double interpolationPoint;
        private int degreeOfPolynomial;
        private List<List<((double, double), double)>> dividedDifferences;

        public AlgebraicInterpolation(List<(double, double)> interpolationNodes, double interpolationPoint,
                int degreeOfPolynomial, IFunction function)
        {
            this.function = function;
            this.interpolationNodes = interpolationNodes;
            this.interpolationPoint = interpolationPoint;
            this.degreeOfPolynomial = degreeOfPolynomial;

            dividedDifferences = new List<List<((double, double), double)>>();

            for (var i = 0; i < degreeOfPolynomial; ++i)
            {
                dividedDifferences.Add(new List<((double, double), double)>());
            }

            SortNodes();
        }

        public double Value()
        {
            SortNodes();
            CalculateDividedDifferences();

            return NewtonPolynomialValue();
        }

        public void PrintNodesForInterpolation()
        {
            Console.WriteLine("Интерполяционные узлы:");
            Console.WriteLine("  x  |  f(x)");

            for (var i = 0; i < degreeOfPolynomial + 1; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i + 1}) ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{interpolationNodes[i].Item1} | {interpolationNodes[i].Item2}");
            }

            Console.WriteLine();
            Console.ResetColor();
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
                    for (var j = 0; j < degreeOfPolynomial - i; ++j)
                    {
                        dividedDifferences[i].Add((((dividedDifferences[i - 1])[j].Item1.Item1, (dividedDifferences[i - 1])[j + 1].Item1.Item2),
                                (dividedDifferences[i - 1][j + 1].Item2 - dividedDifferences[i - 1][j].Item2) / (dividedDifferences[i - 1][j + 1].Item1.Item2 - dividedDifferences[i - 1][j].Item1.Item1)));
                    }
                }
            }
        }

        private void SortNodes()
        {
            interpolationNodes.Sort(((double, double) point1, (double, double) point2) =>
                    Math.Abs(point1.Item1 - interpolationPoint).CompareTo(Math.Abs(point2.Item1 - interpolationPoint)));
        }
            
    }
}
