using AlgsAndDataStructures.DataStructures.CustomQueue;
using AlgsAndDataStructures.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AlgsAndDataStructures.Services;

/// <summary>
/// Интерфейс для доступа к числовому хранилищу
/// </summary>
public interface INumbersStorageService
{
    /// <summary>
    /// Получить текущую колекцию чисел
    /// </summary>
    /// <returns></returns>
    IEnumerable<int> Get();

    /// <summary>
    /// Получить копию текущей коллекции чисел в виде <see cref="CustomList{T}"/>
    /// </summary>
    /// <returns></returns>
    CustomList<int> GetAsCustomList();

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

public class NumbersStorageService : INumbersStorageService
{
    private readonly INumbersStorageRepository _numbersStorageRepository;
    public NumbersStorageService(IServiceProvider serviceProvider)
    {
        _numbersStorageRepository = serviceProvider.GetRequiredService<INumbersStorageRepository>();
    }

    public IEnumerable<int> Get() => _numbersStorageRepository.Get();

    public CustomList<int> GetAsCustomList() => _numbersStorageRepository.GetAsCustomList();

    public int? GetFirst() => _numbersStorageRepository.GetFirst();
    public int? GetLast() => _numbersStorageRepository.GetLast();

    public void Add(int number)
    {
        _numbersStorageRepository.Add(number);
    }

    public void Set(IEnumerable<int> numbers)
    {
        _numbersStorageRepository.Set(numbers);
    }
}
