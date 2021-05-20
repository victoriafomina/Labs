using System;
using System.Collections.Generic;
using System.Text;

namespace Laba6_CauchyProblem
{
    public class MyFunction : IFunction
    {
        public double Value(double x)
        {
            return 0.5 * (x + 3 * Math.Exp(-x) - 1);
        }

        public double DerivativeValue(int derivativeOrder, double x, double y)
        {
            if (derivativeOrder == 0)
            {
                return y;
            }
            else if (derivativeOrder == 1)
            {
                return FirstDerivativeValue(x, y);
            }
            else if (derivativeOrder == 2)
            {
                return SecondDerivativeNumber(x, y);
            }
            else if (derivativeOrder == 3)
            {
                return ThirdDerivativeNumber(x, y);
            }
            else // derivative number is 4
            {
                return FourthDerivativeNumber(x, y);
            }
        }

        private double FirstDerivativeValue(double x, double y)
        {
            return -y + x / 2;
        }

        private double SecondDerivativeNumber(double x, double y)
        {
            return -FirstDerivativeValue(x, y) + 0.5;
        }

        private double ThirdDerivativeNumber(double x, double y)
        {
            return -SecondDerivativeNumber(x, y);
        }

        private double FourthDerivativeNumber(double x, double y)
        {
            return -ThirdDerivativeNumber(x, y);
        }
    }
}
