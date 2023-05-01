namespace AlgsAndDataStructures.Services;

public interface ISortingService<TType>
{
    /// <summary>
    /// Сортирует коллекцию алгоритмом пузырьковой сортировки
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public IEnumerable<TType> BubbleSort(IEnumerable<TType> data);

    /// <summary>
    /// Сортирует коллекцию более продвинутой версией алгоритма пузырьковой сортировки, с проверкой обмена
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public IEnumerable<TType> BubbleSortWithSwaps(IEnumerable<TType> data);
}
