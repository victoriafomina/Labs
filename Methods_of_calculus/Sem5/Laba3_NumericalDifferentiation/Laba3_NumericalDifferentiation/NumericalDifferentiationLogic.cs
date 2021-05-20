using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NumericalDifferentiation
{
    public class NumericalDifferentiationLogic
    {
        private IFunction function;
        private IFunction firstDerivative;
        private IFunction secondDerivative;
        private List<(double, double)> nodes;
        private List<(double, double)> firstNumericalDerivatives;
        private List<(double, double)> firstNumericalDerivativesDeviations;
        private List<(double, double)> secondNumericalDerivatives;
        private List<(double, double)> secondNumericalDerivativesDeviations;
        private double step;

        public NumericalDifferentiationLogic(IFunction function, IFunction firstDerivative, IFunction secondDerivative)
        {
            nodes = new List<(double, double)>();
            firstNumericalDerivatives = new List<(double, double)>();
            firstNumericalDerivativesDeviations = new List<(double, double)>();
            secondNumericalDerivatives = new List<(double, double)>();
            secondNumericalDerivativesDeviations = new List<(double, double)>();
            this.function = function;
            this.firstDerivative = firstDerivative;
            this.secondDerivative = secondDerivative;
        }

        public void CreateTable(double leftBorder, double step, int numberOfPoints)
        {
            nodes.Clear();
            this.step = step;
            
            for (var i = 0; i < numberOfPoints; ++i)
            {
                nodes.Add((leftBorder + i * step, function.Value(leftBorder + i * step)));
            }
        }

        public void PrintTable()
        {
            Console.WriteLine("№ |  x  | f(x)");

            for (var i = 0; i < nodes.Count; ++i)
            {
                Console.WriteLine($"{i + 1}) {nodes[i].Item1} | {nodes[i].Item2}");
            }

            Console.WriteLine();
        }

        public void Run()
        {
            CalculateNumericalDerivatives();
            CalculateDeviations();
            PrintInfoTable();
        }

        private void PrintInfoTable()
        {
            Console.WriteLine("  x  /  f(x)  / f'(x)чд /  |f'(x)-f'(x)чд|  / f''(x)чд /  |f''(x)-f''(x)чд|");
            for (var i = 0; i < nodes.Count; ++i)
            {
                if (i == 0 || i == nodes.Count - 1)
                {
                    Console.WriteLine($"{i + 1}) {nodes[i].Item1} | {nodes[i].Item2} | {firstNumericalDerivatives[i].Item2} | " +
                            $"{firstNumericalDerivativesDeviations[i].Item2} |  -  | -");
                }
                else
                {
                    Console.WriteLine($"{i + 1}) {nodes[i].Item1} | {nodes[i].Item2} | {firstNumericalDerivatives[i].Item2} | " +
                            $"{firstNumericalDerivativesDeviations[i].Item2} |  {secondNumericalDerivatives[i].Item2}  | {secondNumericalDerivativesDeviations[i].Item2}");
                }
            }

            Console.WriteLine();
        }

        private void CalculateNumericalDerivatives()
        {
            CalculateFirstNumericalDerivatives();
            CalculateSecondNumericalDerivatives();
        }

        private void CalculateDeviations()
        {
            CalculateFirstNumericalDerivativesDeviations();
            CalculateSecondNumericalDerivativesDeviations();
        }

        private void CalculateFirstNumericalDerivativesDeviations()
        {
            firstNumericalDerivativesDeviations.Clear();

            for (var i = 0; i < firstNumericalDerivatives.Count; ++i)
            {
                double point = firstNumericalDerivatives[i].Item1;
                firstNumericalDerivativesDeviations.Add((point, Math.Abs(firstDerivative.Value(point) - firstNumericalDerivatives[i].Item2)));
            }
        }

        private void CalculateSecondNumericalDerivativesDeviations()
        {
            secondNumericalDerivativesDeviations.Clear();
            secondNumericalDerivativesDeviations.Add((-1, -1));

            for (var i = 1; i < nodes.Count - 1; ++i)
            {
                double point = secondNumericalDerivatives[i].Item1;
                secondNumericalDerivativesDeviations.Add((point, Math.Abs(secondDerivative.Value(point) - secondNumericalDerivatives[i].Item2)));
            }
        }

        private void CalculateFirstNumericalDerivatives()
        {
            double firstNumericalDerivativeValue;
            firstNumericalDerivatives.Clear();

            for (var i = 0; i < nodes.Count; ++i)
            {
                if (i < nodes.Count - 2)
                {
                    firstNumericalDerivativeValue = (-3 * nodes[i].Item2 + 4 * nodes[i + 1].Item2 - nodes[i + 2].Item2) / (2 * step);
                }
                else
                {
                    firstNumericalDerivativeValue = (3 * nodes[i].Item2 - 4 * nodes[i - 1].Item2 + nodes[i - 2].Item2) / (2 * step);
                }

                firstNumericalDerivatives.Add((nodes[i].Item1, firstNumericalDerivativeValue));
            }
        }

        private void CalculateSecondNumericalDerivatives()
        {
            double secondNumericalDerivativeValue;
            secondNumericalDerivatives.Clear();
            secondNumericalDerivatives.Add((-1, -1));

            for (var i = 1; i < nodes.Count - 1; ++i)
            {
                secondNumericalDerivativeValue = (nodes[i + 1].Item2 - 2 * nodes[i].Item2 + nodes[i - 1].Item2) / (step * step);
                secondNumericalDerivatives.Add((nodes[i].Item1, secondNumericalDerivativeValue));
            }
        }
    }
}
