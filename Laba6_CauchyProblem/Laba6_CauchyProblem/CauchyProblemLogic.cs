using System;
using System.Collections.Generic;
using System.Text;

namespace Laba6_CauchyProblem
{
    public class CauchyProblemLogic
    {
        private int x;
        private int y;
        private IFunction function;
        private List<double> zeroFinalDifferences;
        private List<double> firstFinalDifferences;
        private List<double> secondFinalDifferences;
        private List<double> thirdFinalDifferences;
        private List<double> fourthFinalDifferences;

        public CauchyProblemLogic(int x, int y, IFunction function)
        {
            this.x = x;
            this.y = y;
            this.function = function;
            zeroFinalDifferences = new List<double>();
            firstFinalDifferences = new List<double>();
            secondFinalDifferences = new List<double>();
            thirdFinalDifferences = new List<double>();
            fourthFinalDifferences = new List<double>();
        }

        public void TaylorPolynomialValue(int currentX)
        {
            int highestDerivativeOrder = 4;
            double taylorPolynomialValue = 0;
            int factorial = 1;
            int deviationBracket = 1;

            for (var i = 0; i <= highestDerivativeOrder; ++i)
            {
                if (i > 0)
                {
                    factorial *= i;
                }
                
                taylorPolynomialValue += function.DerivativeValue(i, x, y) * deviationBracket / factorial;
                deviationBracket *= currentX - x;
            }
        }

        public double EulersMethod(double previousX, double previousY, double step)
        {
            return previousY + step * function.DerivativeValue(1, previousX, previousY);
        }

        public double EulersMethodI(double previousX, double previousY, double step)
        {
            return previousY + step * function.DerivativeValue(1, previousX + step / 2.0, previousY + step / 2.0 * function.DerivativeValue(1, previousX, previousY));
        }

        public double EulersMethodII(double previousX, double previousY, double step)
        {
            double firstDerivativeValueInPreviousPoint = function.DerivativeValue(1, previousX, previousY);
            return previousY + step / 2.0 * (firstDerivativeValueInPreviousPoint + function.DerivativeValue(1, previousX + step, previousY + step * firstDerivativeValueInPreviousPoint));
        }

        public double RungeKuttaMethod(double previousX, double previousY, double step)
        {
            double coeff1 = step * function.DerivativeValue(1, previousX, previousY);
            double coeff2 = step * function.DerivativeValue(1, previousX + step / 2.0, previousY + coeff1 / 2.0);
            double coeff3 = step * function.DerivativeValue(1, previousX + step / 2.0, previousY + coeff2 / 2.0);
            double coeff4 = step * function.DerivativeValue(1, previousX + step, previousY + coeff3);

            return previousY + 1.0 / 6 * (coeff1 + 2 * coeff2 + 2 * coeff3 + coeff4);
        }

        public double AdamsMethod(double step, List<(double, double)> points)
        {
            CalculateFinalDifferences(points, step);

            return points[points.Count - 1].Item2 + zeroFinalDifferences[zeroFinalDifferences.Count - 1] + 1.0 / 2 * firstFinalDifferences[firstFinalDifferences.Count - 1] +
                    5.0 / 12 * secondFinalDifferences[secondFinalDifferences.Count - 1] + 3.0 / 8 * thirdFinalDifferences[thirdFinalDifferences.Count - 1] +
                    251.0 / 720 * fourthFinalDifferences[fourthFinalDifferences.Count - 1];
        }

        private void CalculateFinalDifferences(List<(double, double)> points, double step)
        {
            zeroFinalDifferences.Clear();
            firstFinalDifferences.Clear();
            secondFinalDifferences.Clear();
            thirdFinalDifferences.Clear();
            fourthFinalDifferences.Clear();

            for (var i = 0; i < points.Count; ++i)
            {
                zeroFinalDifferences.Add(step * function.DerivativeValue(1, points[i].Item1, points[i].Item2));
            }

            for (var i = 0; i < zeroFinalDifferences.Count - 1; ++i)
            {
                firstFinalDifferences.Add(zeroFinalDifferences[i + 1] + zeroFinalDifferences[i]);
            }

            for (var i = 0; i < firstFinalDifferences.Count - 1; ++i)
            {
                secondFinalDifferences.Add(firstFinalDifferences[i + 1] + firstFinalDifferences[i]);
            }

            for (var i = 0; i < secondFinalDifferences.Count - 1; ++i)
            {
                thirdFinalDifferences.Add(secondFinalDifferences[i + 1] + secondFinalDifferences[i]);
            }

            for (var i = 0; i < thirdFinalDifferences.Count - 1; ++i)
            {
                fourthFinalDifferences.Add(thirdFinalDifferences[i + 1] + thirdFinalDifferences[i]);
            }
        }
    }
}
