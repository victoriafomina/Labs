using System;
using System.Collections.Generic;
using System.Text;

namespace Laba5_ApproxCalcOfIntegralsQFHighestDegree
{
    public class ApproximateCalculationLogic
    {
        private IFunction function;
        private IFunction weightFunction;
        private IFunction functionMehler;
        private double[] moments;
        private double A1;
        private double A2;

        public ApproximateCalculationLogic(IFunction function, IFunction weightFunction, IFunction functionMehler)
        {
            moments = new double[4];
            this.function = function;
            this.weightFunction = weightFunction;
            this.functionMehler = functionMehler;
        }

        public double GaussCompound(int parts, double leftBorder, double rightBorder)
        {
            double result = 0;
            double step = (rightBorder - leftBorder) / parts;

            for (var i = 0; i < parts; ++i)
            {
                double firstPoint = step / 2 * (-1 / Math.Sqrt(3)) + leftBorder + i * step + step / 2;
                double secondPoint = step / 2 * (1 / Math.Sqrt(3)) + leftBorder + i * step + step / 2;
                result += step / 2 * (weightFunction.Value(firstPoint) * function.Value(firstPoint) +
                        weightFunction.Value(secondPoint) * function.Value(secondPoint));
            }

            return result;
        }

        public double GaussianQuadratureFormula(double leftBorder, double rightBorder)
        {
            CalculateMoments(leftBorder, rightBorder);

            double a1 = (moments[0] * moments[3] - moments[2] * moments[1]) / (moments[1] * moments[1] - moments[2] * moments[0]);
            double a2 = (moments[2] * moments[2] - moments[3] * moments[1]) / (moments[1] * moments[1] - moments[2] * moments[0]);

            // узлы
            double x1 = (-a1 + Math.Sqrt(a1 * a1 - 4 * a2)) / 2;
            double x2 = (-a1 - Math.Sqrt(a1 * a1 - 4 * a2)) / 2;

            // коэффициенты квадратурной формулы
            A1 = 1 / (x1 - x2) * (moments[1] - x2 * moments[0]);
            A2 = 1 / (x2 - x1) * (moments[1] - x1 * moments[0]);

            return A1 * Math.Sin(x1) + A2 * Math.Sin(x2);
        }

        public void PrintQuadratureFormulasCoefficients()
        {
            Console.WriteLine("Коэффициенты квадратурной формулы:");
            Console.WriteLine($"A1 = {A1}");
            Console.WriteLine($"A2 = {A2}");
        }

        private void CalculateMoments(double leftBorder, double rightBorder)
        {
            moments[0] = MediumRectangles(leftBorder, rightBorder, 0);
            moments[1] = MediumRectangles(leftBorder, rightBorder, 1);
            moments[2] = MediumRectangles(leftBorder, rightBorder, 2);
            moments[3] = MediumRectangles(leftBorder, rightBorder, 3);
        }

        private double MediumRectangles(double leftBorder, double rightBorder, int indexOfMoment)
        {
            double value = 0;
            double step = (rightBorder - leftBorder) / 10000;
            for (var i = 0; i < 10000; ++i)
            {
                value += PowersOfX(indexOfMoment, leftBorder + (i + 0.5) * step);
            }

            return value * step;
        }

        public void PrintMoments()
        {
            Console.WriteLine("Моменты:");
            for (var i = 0; i < moments.Length; ++i)
            {
                Console.WriteLine($"m{i} = {moments[i]}");
            }
        }

        private double PowersOfX(double indexOfMoment, double point)
        {
            double x = 1;

            for (var i = 0; i < indexOfMoment; ++i)
            {
                x *= point;
            }

            return Math.Cos(point) * Math.Cos(point) * x;
        }

        public double MehlersFormula(int numberOfNodes)
        {
            double result = 0;

            for (var i = 0; i < numberOfNodes; ++i)
            {
                result += functionMehler.Value(Math.Cos((2 * i + 1) / (2 * numberOfNodes) * Math.PI));
                Console.WriteLine($"Узел {i} КФ Мелера: {Math.Cos((2 * i + 1.0) / (2 * numberOfNodes) * Math.PI)}");
                Console.WriteLine($"Коэффициент {i} КФ Мелера: {Math.PI / numberOfNodes}\n");
            }

            result *= Math.PI / numberOfNodes;

            return result;
        }
    }
}
