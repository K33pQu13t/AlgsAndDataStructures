using System.Numerics;

namespace AlgsAndDataStructures.Services.Puzzle;

public interface IFibonacciSolverService
{
    /// <summary>
    /// Получить число, которое получится после прохода указанного числа итераций алгоритма Фибоначчи
    /// </summary>
    /// <param name="countOfIterations"></param>
    /// <returns></returns>
    BigInteger Solve(int countOfIterations);
}

public class FibonacciSolverService : IFibonacciSolverService
{
    public BigInteger Solve(int countOfIterations)
    {
        if (countOfIterations == 0)
        {
            return 0;
        }
        if (countOfIterations == 1)
        {
            return 1;
        }

        return FibonaciiRecursion(--countOfIterations, 0, 1, 1);
    }

    protected BigInteger FibonaciiRecursion(int iterationsLeft, BigInteger latest, BigInteger prelatest, BigInteger current)
    {
        return --iterationsLeft == 0
            ? current
            : FibonaciiRecursion(
                iterationsLeft, 
                latest: prelatest, 
                prelatest: latest + prelatest, 
                current: latest + prelatest + prelatest
            );
    }
}
