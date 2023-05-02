namespace AlgsAndDataStructures.Services.Sorting;

public interface ISortingService<TType>
{
    /// <summary>
    /// Отсортировать коллекцию алгоритмом пузырьковой сортировки
    /// </summary>
    /// <param name="data"></param>
    public IEnumerable<TType> BubbleSort(IEnumerable<TType> data);

    /// <summary>
    /// Отсортировать коллекцию более продвинутой версией алгоритма пузырьковой сортировки, с проверкой обмена
    /// </summary>
    /// <param name="data"></param>
    public IEnumerable<TType> BubbleSortWithSwapChecks(IEnumerable<TType> data);

    /// <summary>
    /// Отсортировать коллекцию алгоритмом сортировки перемешиванием
    /// </summary>
    /// <param name="data"></param>
    public IEnumerable<TType> ShufflingSort(IEnumerable<TType> data);

    /// <summary>
    /// Отсортировать коллекцию более продвинутой версией алгоритма сортировки перемешиванием, с проверкой обмена
    /// </summary>
    /// <param name="data"></param>
    public IEnumerable<TType> ShufflingSortWithSwapChecks(IEnumerable<TType> data);

    /// <summary>
    /// Отсортировать коллекцию алгоритмов сортировкой вставками
    /// </summary>
    /// <param name="data"></param>
    public IEnumerable<TType> InsertionSort(IEnumerable<TType> data);

    /// <summary>
    /// Отсортировать коллекцию гномьей сортировкой
    /// </summary>
    /// <param name="data"></param>
    public IEnumerable<TType> GnomeSort(IEnumerable<TType> data);
}
