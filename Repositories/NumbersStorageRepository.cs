using System.Collections.Immutable;

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
    private IImmutableList<int> _numbers = ImmutableList.Create<int>();

    public IEnumerable<int> Get() => _numbers;

    public void Set(IEnumerable<int> numbers)
    {
        _numbers = ImmutableList.CreateRange<int>(numbers);
    } 

    public void Add(int number)
    {
        _numbers.Insert(_numbers.Count-1, number);
    }
}
