using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_Newtons_Method
{
    public static class Utils
    {
        public static void PrintTable(List<(double, double)> points, int degreeOfPolynomial)
        {
            for (var i = 0; i < degreeOfPolynomial + 1; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i + 1}) ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"x = {points[i].Item1} | f(x) = {points[i].Item2}");
            }
            Console.ResetColor();
        }
    }
}
