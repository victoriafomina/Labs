using System;
using System.Collections.Generic;
using System.Text;

namespace Laba1_2Systems_GaussMs_JordanM_LUexpansion
{
    public static class MatricesUtils
    {
        public static double[] GaussMethod(double[,] matrix)
        {
            int matrixDimension = matrix.GetLength(0); // Размерность начальной матрицы (строки)
            double[,] matrixDuplicate = new double[matrixDimension, matrixDimension + 1]; // Матрица-дублер

            for (int i = 0; i < matrixDimension; ++i)
            {
                for (int j = 0; j < matrixDimension + 1; ++j)
                {
                    matrixDuplicate[i, j] = matrix[i, j];
                }               
            }
                
            // Прямой ход (Зануление нижнего левого угла)
            for (int k = 0; k < matrixDimension; ++k) // k-номер строки
            {
                for (int i = 0; i < matrixDimension + 1; ++i) // i-номер столбца
                {
                    matrixDuplicate[k, i] = matrixDuplicate[k, i] / matrix[k, k]; 
                    // Деление k-строки на первый член !=0 для преобразования его в единицу
                }
                   
                for (int i = k + 1; i < matrixDimension; ++i) // i-номер следующей строки после k
                {
                    double coefficient = matrixDuplicate[i, k] / matrixDuplicate[k, k]; // Коэффициент

                    for (int j = 0; j < matrixDimension + 1; ++j) // j-номер столбца следующей строки после k
                    {
                        matrixDuplicate[i, j] = matrixDuplicate[i, j] - matrixDuplicate[k, j] * coefficient; 
                        // Зануление элементов матрицы ниже первого члена, преобразованного в единицу
                    }                        
                }

                for (int i = 0; i < matrixDimension; ++i) // Обновление, внесение изменений в начальную матрицу
                {
                    for (int j = 0; j < matrixDimension + 1; ++j)
                    {
                        matrix[i, j] = matrixDuplicate[i, j];
                    }
                }                    
            }

            // Обратный ход (Зануление верхнего правого угла)
            for (int k = matrixDimension - 1; k > -1; --k) // k-номер строки
            {
                for (int i = matrixDimension; i > -1; --i) // i-номер столбца
                {
                    matrixDuplicate[k, i] = matrixDuplicate[k, i] / matrix[k, k];
                }
                    
                for (int i = k - 1; i > -1; --i) // i-номер следующей строки после k
                {
                    double K = matrixDuplicate[i, k] / matrixDuplicate[k, k];

                    for (int j = matrixDimension; j > -1; --j) // j-номер столбца следующей строки после k
                    {
                        matrixDuplicate[i, j] = matrixDuplicate[i, j] - matrixDuplicate[k, j] * K;
                    }
                }
            }

            // Отделяем от общей матрицы ответы
            double[] answer = new double[matrixDimension];

            for (int i = 0; i < matrixDimension; ++i)
            {
                answer[i] = matrixDuplicate[i, matrixDimension];
            }                    

            return answer;
        }

        private static double MatrixNorm(double[,] matrix)
        {
            int matrixDimension = matrix.GetLength(0);
            double norm = 0;

            for (var j = 0; j < matrixDimension; ++j)
            {
                double temp = 0;

                for (var i = 0; i < matrixDimension; ++i)
                {
                    temp += Math.Abs(matrix[i, j]);
                }

                if (temp > norm)
                {
                    norm = temp;
                }
            }

            return norm;
        }

        public static double[,] ReverseMatrix(double[,] matrix)
        {
            double det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            if (Math.Abs(det) < 0.0000001)
            {
                throw new Exception("Determinant is 0! Reverse matrix does not exist!");
            }

            // Получаем транспонированную матрицу
            double[,] transposedMatrix = matrix;
            double temp;
            temp = transposedMatrix[0, 1];
            transposedMatrix[0, 1] = transposedMatrix[1, 0];
            transposedMatrix[1, 0] = temp;

            // А теперь получим из нее обратную матрицу
            for (var i = 0; i < 2; ++i)
            {
                for (var j = 0; j < 2; ++j)
                {
                    transposedMatrix[i, j] /= det;
                }
            }

            return transposedMatrix;
        }

        public static double ConditionNumber(double[,] matrix)
        {
            return MatrixNorm(ReverseMatrix(matrix)) * MatrixNorm(matrix);
        }
    }
}
