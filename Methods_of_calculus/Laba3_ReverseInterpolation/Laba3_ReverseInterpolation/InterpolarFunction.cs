using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_ReverseInterpolation
{
    public class InterpolarFunction : IFunction
    {
        private double value;
        private AlgebraicInterpolation polynomial;
        private List<(double, double)> interpolationNodes;
        private int degreeOfPolynomial;
        private IFunction function;
        private bool printTable;

        public InterpolarFunction(double value)
        {
            this.value = value;
            printTable = true;
        }

        public void SetValue(double value) => this.value = value;

        public void SetInterpolationNodes(List<(double, double)> nodes) => this.interpolationNodes = nodes;

        public void SetDegreeOfPolynomial(int degree) => this.degreeOfPolynomial = degree;

        public void SetFunction(IFunction function) => this.function = function;
             
        public double Value(double x)
        {
            // а функция передается для вычисления погрешности в старой лабе, что уже довольно костыльно
            polynomial = new AlgebraicInterpolation(interpolationNodes, x, degreeOfPolynomial, function);

            if (printTable)
            {
                printTable = false;
                polynomial.PrintSortedTable();
            }

            return polynomial.Run() - value;
        }
    }
}
