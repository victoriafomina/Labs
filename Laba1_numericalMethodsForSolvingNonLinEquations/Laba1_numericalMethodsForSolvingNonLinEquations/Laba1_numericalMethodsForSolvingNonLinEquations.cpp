#include <iostream>
#include <cmath>

using namespace std;

// 12 вариант

void separationOfRoots();
double function(double point);
double derivativeOfAFunction(double point);
void separationOfRoots();
void bisection(double leftFrontier, double rightFrontier, double eps);
void newtonsMethod(double leftFrontier, double rightFrontier, double eps);
void modifiedNewtonsMethod(double leftFrontier, double rightFrontier, double const eps);

int main()
{
    setlocale(LC_ALL, "Russian");
    cout << "ЧИСЛЕННЫЕ МЕТОДЫ РЕШЕНИЯ НЕЛИНЕЙНЫХ УРАВНЕНИЙ\n" << endl;

    separationOfRoots();


    return 0;
}

/// <summary>
/// Вычисляет значение функции 2^{-x} + 0,5∙x^2 ‒ 10 в точке.
/// </summary>
double function(double point)
{
    return pow(2, -point) + 0.5 * point * point - 10;
}

/// <summary>
/// Вычисляет значение производной функции 2^{-x} + 0,5∙x^2 ‒ 10 в точке.
/// </summary>
double derivativeOfAFunction(double point)
{
    return -pow(2, -point) + log(2) + 2 * point;
}

/// <summary>
/// Процедура отделения корней.
/// </summary>
void separationOfRoots()
{
    double leftFrontier = 0;
    double rightFrontier = 0;

    cout << "Введите левую границу отрезка: ";
    cin >> leftFrontier;

    cout << "Введите правую границу отрезка: ";
    cin >> rightFrontier;

    int numberOfParts = 0;
    cout << "Введите на сколько частей будет разбит отрезок: ";
    cin >> numberOfParts;

    double step = static_cast<double>(leftFrontier + rightFrontier) / numberOfParts;

    double currentLeftPoint = leftFrontier;
    double currentRightPoint = rightFrontier;
    double currentLeftPointValue = function(currentLeftPoint);
    int countSignChangeIntervals = 0;

    while (currentRightPoint <= rightFrontier)
    {
        double currentRightPointValue = function(currentRightPoint);

        if (currentLeftPointValue * currentRightPointValue <= 0)
        {
            ++countSignChangeIntervals;
            cout << "[" << currentLeftPoint << ", " << currentRightPoint << "]" << endl;

            double const eps = pow(10, -8);
            bisection(currentLeftPoint, currentRightPoint, eps);
        }

        currentLeftPoint = currentRightPoint;
        currentRightPoint += step;
        currentLeftPointValue = currentRightPointValue;
    }

    cout << "Количество отрезков смены знака функции: " << countSignChangeIntervals << endl;
}

/// <summary>
/// Поиск приближенного решения уравнения методом половинного деления.
/// </summary>
void bisection(double leftFrontier, double rightFrontier, double const eps)
{
    cout << "Метод половинного деления\n" << endl;

    do
    {
        double midpoint = (leftFrontier + rightFrontier) / 2;

        if (function(leftFrontier) * function(midpoint) < 0)
        {
            rightFrontier = midpoint;
        }
        else
        {
            leftFrontier = midpoint;
        }
    } 
    while (rightFrontier - leftFrontier > 2 * eps);

    double approximateSolution = (leftFrontier + rightFrontier) / 2;

    cout << "Модуль невязки: " << abs(function(approximateSolution)) << endl;
}

/// <summary>
/// Поиск приближенного решения уравнения методом Ньютона (методом касательных).
/// </summary>
void newtonsMethod(double leftFrontier, double rightFrontier, double const eps)
{
    cout << "Метод Ньютона (метод касательных)\n" << endl;

    double nthPoint = (leftFrontier + rightFrontier) / 2;

    int countStepsToGetApproximateSolution = 0;

    while (abs(function(nthPoint)) > eps)
    {
        nthPoint -= function(nthPoint) / derivativeOfAFunction(nthPoint);
        ++countStepsToGetApproximateSolution;
    }

    cout << "Количество шагов, потребовавшееся для нахождения приближенного решения: " <<
            countStepsToGetApproximateSolution << endl;
    cout << "Приближенное решение: " << function(nthPoint) << endl;
    cout << "Модуль невязки: " << abs(function(nthPoint)) << endl;
}

/// <summary>
/// Поиск приближенного решения уравнения модифицированным методом Ньютона.
/// </summary>
void modifiedNewtonsMethod(double leftFrontier, double rightFrontier, double const eps)
{
    cout << "Модифицированный метод Ньютона\n" << endl;

    double firstPoint = (leftFrontier + rightFrontier) / 2;
    double nthPoint = firstPoint;

    int countStepsToGetApproximateSolution = 0;

    while (abs(function(nthPoint)) > eps)
    {
        nthPoint -= function(nthPoint) / derivativeOfAFunction(firstPoint);
        ++countStepsToGetApproximateSolution;
    }

    cout << "Количество шагов, потребовавшееся для нахождения приближенного решения: " <<
        countStepsToGetApproximateSolution << endl;
    cout << "Приближенное решение: " << function(nthPoint) << endl;
    cout << "Модуль невязки: " << abs(function(nthPoint)) << endl;
}

/// <summary>
/// Поиск приближенного решения уравнения методом секущих.
/// </summary>
void secantMethod(double leftFrontier, double rightFrontier, double const eps)
{
    cout << "Метод секущих\n" << endl;

    // Два предыдущих, откуда?
    double prePreviousPoint = (leftFrontier + rightFrontier) / 2;
    double previousPoint = (leftFrontier + rightFrontier) / 2;
    double currentPoint = 0;

    while (abs(function(previousPoint)) > eps)
    {
        currentPoint = previousPoint - function(previousPoint) /
                (function(previousPoint) - function(prePreviousPoint)) * (previousPoint - prePreviousPoint);
    }

}