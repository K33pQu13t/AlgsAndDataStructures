using AlgsAndDataStructures.DataStructures.CustomQueue;

namespace AlgsAndDataStructures.Repositories;

/// <summary>
/// Интерфейс числового хранилища
/// </summary>
public interface INumbersStorageRepository
{
    /// <summary>
    /// Получить текущую колекцию чисел
    /// </summary>
    /// <returns></returns>
    IEnumerable<int> Get();

    /// <summary>
    /// Получить первое число текущей коллекции
    /// </summary>
    /// <returns></returns>
    int? GetFirst();

    /// <summary>
    /// Получить последнее число текущей коллекции
    /// </summary>
    /// <returns></returns>
    int? GetLast();

    /// <summary>
    /// Перезаписать текущую колекцию чисел
    /// </summary>
    /// <param name="numbers"></param>
    void Set(IEnumerable<int> numbers);

    /// <summary>
    /// Добавить в конец текущей коллекции чисел новое число
    /// </summary>
    /// <param name="number"></param>
    void Add(int number);
}

public class NumbersStorageRepository : INumbersStorageRepository
{
    private CustomList<int?> _numbers = new();

    public IEnumerable<int> Get() => _numbers.AsEnumerable().Cast<int>();

    public int? GetFirst() => _numbers.First();

    public int? GetLast() => _numbers.Last();

    public void Set(IEnumerable<int> numbers)
    {
        _numbers = new(numbers.Cast<int?>());
    } 

    public void Add(int number)
    {
        _numbers.Add(number);
    }
}
