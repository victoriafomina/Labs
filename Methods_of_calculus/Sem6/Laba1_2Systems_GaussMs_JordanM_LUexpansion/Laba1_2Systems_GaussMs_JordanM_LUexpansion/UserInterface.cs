using System;
using System.Collections.Generic;
using System.Text;

namespace Laba1_2Systems_GaussMs_JordanM_LUexpansion
{
    public static class UserInterface
    {
        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("ПЕРВАЯ ЛАБОРАТОНАЯ РАБОТА\nВАРИАНТ I");
                Console.WriteLine("0 - уходим отседава");
                Console.WriteLine("1 - посмотреть решение первой задачи:\nрешить две системы (одну с “точной” правой частью, " +
                    "вторую с изменённой), найти число обусловленности исходной матрицы, посчитать фактическую погрешность и " +
                    "оценку погрешности");
            }            
        }

        public static void FirstTask()
        {
            var matrixWithExactRightSide = new double[,] { { -400.60, 199.80, 200 }, { 1198.80, -600.40, -600 } };
            var matrixWithModifiedRightSide = new double[,] { { -400.60, 199.80, 199 }, { 1198.80, -600.40, -601 } };

            double[] matrixWithExactRightSideSolution =
                    MatricesUtils.GaussMethod(matrixWithExactRightSide);

            double[] matrixWithModifiedRightSideSolution =
                    MatricesUtils.GaussMethod(matrixWithModifiedRightSide);

            // condition number - число обусловленности

            Console.WriteLine($"Матрица с \"точной\" правой частью:");
            Console.WriteLine($"{matrixWithExactRightSide[0, 0]}    {matrixWithModifiedRightSide[0, 1]}");
            Console.WriteLine($"{matrixWithExactRightSide[1, 0]}    {matrixWithModifiedRightSide[1, 1]}\n");

            Console.WriteLine($"Матрица с \"измененной\" правой частью:");
            Console.WriteLine($"{matrixWithModifiedRightSide[0, 0]}    {matrixWithModifiedRightSide[0, 1]}");
            Console.WriteLine($"{matrixWithModifiedRightSide[1, 0]}    {matrixWithModifiedRightSide[1, 1]}\n");

            Console.WriteLine($"Число обусловленности исходной матрицы: {MatricesUtils.ConditionNumber(matrixWithExactRightSide)}\n");

            var differenceSolutionVector = new[] { matrixWithModifiedRightSideSolution[0] - matrixWithExactRightSideSolution[0],
                    matrixWithModifiedRightSideSolution[1] - matrixWithExactRightSideSolution[1] };
            var differenceSolutionVectorNorm = Math.Sqrt(differenceSolutionVector[0] * differenceSolutionVector[0] + differenceSolutionVector[1] * differenceSolutionVector[1]);
            var exactRightSideSolutionNorm = Math.Sqrt(matrixWithModifiedRightSideSolution[0] * matrixWithModifiedRightSideSolution[0] +
                    matrixWithModifiedRightSideSolution[1] + matrixWithModifiedRightSideSolution[1]);
            Console.WriteLine($"Фактическая относительная погрешность: {differenceSolutionVectorNorm / exactRightSideSolutionNorm}");

            var rightSideDifferenceVectorNorm = Math.Abs(Math.Pow(matrixWithModifiedRightSide[0, 2] - matrixWithExactRightSide[0, 2], 2) +
                    Math.Pow(matrixWithModifiedRightSide[1, 2] - matrixWithExactRightSide[1, 2], 2));
            var rightSideExactVectorNorm = Math.Abs(Math.Pow(matrixWithExactRightSide[0, 2], 2) + Math.Pow(matrixWithExactRightSide[1, 2], 2));
            Console.WriteLine($"Оценка погрешности: ||delta(x)|| / ||x*|| <= " +
                    $"{MatricesUtils.ConditionNumber(matrixWithExactRightSide) * rightSideDifferenceVectorNorm / rightSideExactVectorNorm}");
        }

    }
}
