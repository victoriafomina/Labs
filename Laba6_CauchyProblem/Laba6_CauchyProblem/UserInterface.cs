using System;
using System.Collections.Generic;
using System.Text;

namespace Laba6_CauchyProblem
{
    public class UserInterface
    {
        private List<(double, double)> exactSolutions;
        private double step = 0.1;
        private int N = 10;
        private int xZero = 0;
        private int yZero = 1;
        private IFunction function;

        public UserInterface(IFunction function)
        {
            this.function = function;
            exactSolutions = new List<(double, double)>();
        }

        private void CalculateExactSolutions()
        {
            exactSolutions.Clear();

            for (var i = -2; i <= N; ++i)
            {
                exactSolutions.Add((xZero + i * step, function.Value(xZero + i * step)));
            }
        }

        private void PrintExactSolutions()
        {
            Console.WriteLine(" x | f(x)");
            
            for (var i = 0; i < exactSolutions.Count; ++i)
            {
                Console.WriteLine($"{exactSolutions[i].Item1} | {exactSolutions[i].Item2}");
            }
            Console.WriteLine();
        }

        private void PrintTaylor()
        {

        }
    }
}
