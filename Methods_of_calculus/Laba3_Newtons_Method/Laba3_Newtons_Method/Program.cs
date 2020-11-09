using System;
using System.Collections.Generic;

namespace Laba3_Newtons_Method
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<(double, double)> list = new List<(double, double)> { (100, 2), (101, Math.Log10(101)),
                    (102, Math.Log10(102)), (103, Math.Log10(103)), (104, Math.Log10(104))};
            AlgebraicInterpolation a = new AlgebraicInterpolation(list, 101.1, 4, new MyFunction());
            a.CalculateDividedDifferences();
        }
    }
}
