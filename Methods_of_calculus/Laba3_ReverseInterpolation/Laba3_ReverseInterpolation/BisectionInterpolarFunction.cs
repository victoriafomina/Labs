using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_ReverseInterpolation
{
    public class BisectionInterpolarFunction : IFunction
    {
        private List<(double, double)> nodes;
        int degreeOfPolynomial;

        public BisectionInterpolarFunction(List<(double, double)> nodes, int degreeOfPolynomial)
        {
            this.nodes = nodes;
            this.degreeOfPolynomial = degreeOfPolynomial;
        }

        public double Value(double interpolationPoint)
        {
            return new AlgebraicInterpolation(nodes, interpolationPoint, degreeOfPolynomial, new MyFunction()).Run();
        }
    }
}
