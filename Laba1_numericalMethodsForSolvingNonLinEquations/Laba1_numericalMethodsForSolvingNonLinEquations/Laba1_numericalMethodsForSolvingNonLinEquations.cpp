#include <iostream>
#include <cmath>

using namespace std;

// 14 вариант

void separationOfRoots(double leftFrontier, double rightFrontier);
double function(double point);
double derivativeOfAFunction(double point);
void bisection(double leftFrontier, double rightFrontier, double eps);
void newtonsMethod(double leftFrontier, double rightFrontier, double eps);
void modifiedNewtonsMethod(double leftFrontier, double rightFrontier, double const eps);
void secantMethod(double leftFrontier, double rightFrontier, double const eps);
void printResultOfSolvingEquation(int stepsToGetApproximateSolution, double approximateSolution);

int main()
{
    setlocale(LC_ALL, "Russian");
    cout << "ЧИСЛЕННЫЕ МЕТОДЫ РЕШЕНИЯ НЕЛИНЕЙНЫХ УРАВНЕНИЙ\n" << endl;

    int number = 0;
    cout << "Введите 0, если не хотите менять границы отрезка" << endl;
    cout << "Введите 1, если хотите изменить границы отрезка" << endl;
    
    cin >> number;

    double leftFrontier = -1;
    double rightFrontier = 3;

    if (number == 1)
    {
        cout << "Введите левую границу отрезка: ";
        cin >> leftFrontier;
        cout << "Введите правую границу отрезка: ";
        cin >> rightFrontier;
    }

    separationOfRoots(leftFrontier, rightFrontier);

    return 0;
}

/// <summary>
/// Вычисляет значение функции 2^{-x} + 0,5∙x^2 ‒ 10 в точке x.
/// </summary>
double function(double x)
{
    return (x - 1) * (x - 1) - exp(-x);
}

/// <summary>
/// Вычисляет значение производной функции 2^{-x} + 0,5∙x^2 ‒ 10 в точке x.
/// </summary>
double derivativeOfAFunction(double x)
{
    return 2 * (x - 1) + exp(-x);
}

/// <summary>
/// Процедура отделения корней.
/// </summary>
void separationOfRoots(double leftFrontier, double rightFrontier)
{
    int numberOfParts = 0;
    cout << "Введите на сколько частей будет разбит отрезок: ";
    cin >> numberOfParts;
    cout << endl;

    double step = static_cast<double>(leftFrontier + rightFrontier) / numberOfParts;

    double currentLeftPoint = leftFrontier;
    double currentRightPoint = leftFrontier + step;
    double currentLeftPointValue = function(currentLeftPoint);
    int countSignChangeIntervals = 0;

    while (currentRightPoint <= rightFrontier)
    {
        double currentRightPointValue = function(currentRightPoint);

        if (currentLeftPointValue * currentRightPointValue <= 0)
        {
            ++countSignChangeIntervals;
            cout << "[" << currentLeftPoint << ", " << currentRightPoint << "]" << endl;

            double const eps = pow(10, -8); // 10 -8 nnnada
            bisection(currentLeftPoint, currentRightPoint, eps);
            newtonsMethod(currentLeftPoint, currentRightPoint, eps);
            modifiedNewtonsMethod(currentLeftPoint, currentRightPoint, eps);
            secantMethod(currentLeftPoint, currentRightPoint, eps);
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
    cout << "Метод половинного деления" << endl;

    int countStepsToGetApproximateSolution = 0;

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

        ++countStepsToGetApproximateSolution;
    } 
    while (rightFrontier - leftFrontier > 2 * eps);

    double approximateSolution = (leftFrontier + rightFrontier) / 2;

    printResultOfSolvingEquation(countStepsToGetApproximateSolution, approximateSolution);
}

/// <summary>
/// Поиск приближенного решения уравнения методом Ньютона (методом касательных).
/// </summary>
void newtonsMethod(double leftFrontier, double rightFrontier, double const eps)
{
    cout << "Метод Ньютона (метод касательных)" << endl;

    double nthPoint = (leftFrontier + rightFrontier) / 2;

    int countStepsToGetApproximateSolution = 0;

    while (abs(function(nthPoint)) > eps)
    {
        nthPoint -= function(nthPoint) / derivativeOfAFunction(nthPoint);
        ++countStepsToGetApproximateSolution;
    }

    printResultOfSolvingEquation(countStepsToGetApproximateSolution, nthPoint);
}

/// <summary>
/// Поиск приближенного решения уравнения модифицированным методом Ньютона.
/// </summary>
void modifiedNewtonsMethod(double leftFrontier, double rightFrontier, double const eps)
{
    cout << "Модифицированный метод Ньютона" << endl;

    double firstPoint = (leftFrontier + rightFrontier) / 2;
    double nthPoint = firstPoint;

    int countStepsToGetApproximateSolution = 0;

    while (abs(function(nthPoint)) > eps)
    {
        nthPoint -= function(nthPoint) / derivativeOfAFunction(firstPoint);
        ++countStepsToGetApproximateSolution;
    }

    printResultOfSolvingEquation(countStepsToGetApproximateSolution, nthPoint);
}

/// <summary>
/// Поиск приближенного решения уравнения методом секущих.
/// </summary>
void secantMethod(double leftFrontier, double rightFrontier, double const eps)
{
    cout << "Метод секущих" << endl;

    double previousPoint = leftFrontier;
    double currentPoint = rightFrontier;
    double nextPoint = 0;

    int countStepsToGetApproximateSolution = 0;

    while (abs(function(currentPoint)) > eps)
    {
        nextPoint = currentPoint - function(currentPoint) /
                (function(currentPoint) - function(previousPoint)) * (currentPoint - previousPoint);
        previousPoint = currentPoint;
        currentPoint = nextPoint;
        ++countStepsToGetApproximateSolution;
    }

    printResultOfSolvingEquation(countStepsToGetApproximateSolution, currentPoint);
}

/// <summary>
/// Выводит на консоль результат решения уравнения.
/// </summary>
void printResultOfSolvingEquation(int stepsToGetApproximateSolution, double approximateSolution)
{
    cout << "Количество шагов, потребовавшееся для нахождения приближенного решения: " <<
        stepsToGetApproximateSolution << endl;
    cout << "Приближенное решение: " << approximateSolution << endl;
    cout << "Модуль невязки: " << abs(function(approximateSolution)) << endl << endl;
}