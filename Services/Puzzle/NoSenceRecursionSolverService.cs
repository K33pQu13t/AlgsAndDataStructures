namespace AlgsAndDataStructures.Services.Puzzle;

/// <summary>
/// Сервис для нахождения значения бессмысленной рекурсивной функции
/// </summary>
public interface INoSenceRecursionSolverService
{
    int NoSenceRecursion(int a, int c, int n);
}

public class NoSenceRecursionSolverService : INoSenceRecursionSolverService
{
    public int NoSenceRecursion(int a, int c, int n)
    {
        if (n >= 0 && n <= 9)
        {
            return n;
        }
        return GetRest(a, c, n) * NoSenceRecursion(a, c, n - 1 - GetRest(a, c, n)) + n;
    }

    private int GetRest(int a, int c, int m) => (a * (m + c)) % 10;
}
