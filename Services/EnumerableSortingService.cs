namespace AlgsAndDataStructures.Services;

public class EnumerableSortingService<T> : ISortingService<T>
{
    protected Comparer<T> _comparer = Comparer<T>.Default;

    /// <inheritdoc/>
    /// <returns>Новая, отсортированная коллекция</returns>
    public IEnumerable<T> BubbleSort(IEnumerable<T> data)
    {
        List<T> sortedData = new(data);
        int count = sortedData.Count;
        for (int index1 = 0; index1 < count - 1; index1++)
        {
            for (int index2 = 0; index2 < count - 1 - index1 ; index2++)
            {
                SwapIfNextIsHigher(sortedData, index2);
            }
        }
        return sortedData;
    }

    /// <inheritdoc/>
    /// <returns>Новая, отсортированная коллекция</returns>
    public IEnumerable<T> BubbleSortWithSwapChecks(IEnumerable<T> data)
    {
        List<T> sortedData = new(data);
        int count = sortedData.Count;
        bool isSwapped;
        for (int index1 = 0; index1 < count - 1; index1++)
        {
            isSwapped = false;
            for (int index2 = 0; index2 < count - 1 - index1; index2++)
            {
                if (SwapIfNextIsHigher(sortedData, index2))
                {
                    isSwapped = true;
                }
            }
            if (!isSwapped)
            {
                break;
            }
        }
        return sortedData;
    }

    /// <inheritdoc/>
    /// <returns>Новая, отсортированная коллекция</returns>
    public IEnumerable<T> ShufflingSort(IEnumerable<T> data)
    {
        List<T> sortedData = new(data);
        bool isSwapped = true;
        int firstIndex = 0;
        int lastIndex = sortedData.Count - 1;

        while (isSwapped)
        {
            isSwapped = false;

            for (int index = firstIndex; index < lastIndex; index++)
            {
                if (SwapIfNextIsHigher(sortedData, index))
                { 
                    isSwapped = true;
                }
            }

            lastIndex--;

            for (int index = lastIndex - 1; index >= firstIndex; index--)
            {
                if (SwapIfNextIsHigher(sortedData, index))
                {
                    isSwapped = true;
                }
            }

            firstIndex++;
        }

        return sortedData;
    }

    /// <inheritdoc/>
    /// <returns>Новая, отсортированная коллекция</returns>
    public IEnumerable<T> ShufflingSortWithSwapChecks(IEnumerable<T> data)
    {
        List<T> sortedData = new(data);
        bool isSwapped = true;
        int firstIndex = 0;
        int lastIndex = sortedData.Count - 1;

        while (isSwapped)
        {
            isSwapped = false;

            for (int index = firstIndex; index < lastIndex; index++)
            {
                if (SwapIfNextIsHigher(sortedData, index))
                {
                    isSwapped = true;
                }
            }

            if (!isSwapped)
            {
                break;
            }

            isSwapped = false;

            lastIndex--;

            for (int index = lastIndex - 1; index >= firstIndex; index--)
            {
                if (SwapIfNextIsHigher(sortedData, index))
                {
                    isSwapped = true;
                }
            }

            firstIndex++;
        }

        return sortedData;
    }

    /// <inheritdoc/>
    /// <returns>Новая, отсортированная коллекция</returns>
    public IEnumerable<T> InsertionSort(IEnumerable<T> data)
    {
        List<T> sortedData = new(data);
        for (int index1 = 1; index1 < sortedData.Count; index1++)
        {
            T keyValue = sortedData[index1];
            int index2 = index1 - 1;

            while (index2 >= 0 && _comparer.Compare(sortedData[index2], keyValue) > 0)
            {
                sortedData[index2 + 1] = sortedData[index2];
                index2--;
            }
            sortedData[index2 + 1] = keyValue;
        }

        return sortedData;
    }

    /// <inheritdoc/>
    /// <returns>Новая, отсортированная коллекция</returns>
    public IEnumerable<T> GnomeSort(IEnumerable<T> data)
    {
        List<T> sortedData = new(data);
        int index = 0;
        while (index < sortedData.Count)
        {
            if (index == 0 || _comparer.Compare(sortedData[index - 1], sortedData[index]) < 0)
            {
                index++;
            }
            else
            {
                SwapIfNextIsHigher(sortedData, index - 1);
                index--;
            }
        }
        return sortedData;
    }

    /// <summary>
    /// Поменять местами соседние значения в коллекции, если второе больше первого. 
    /// Сравнивает значение по текущему индексу со следующим
    /// </summary>
    /// <param name="data"></param>
    /// <param name="index"></param>
    /// <returns><see cref="true"/> если смена была произведена</returns>
    private bool SwapIfNextIsHigher(List<T> data, int index)
    {
        if (_comparer.Compare(data[index], data[index + 1]) > 0)
        {
            (data[index + 1], data[index]) = (data[index], data[index + 1]);
            return true;
        }
        return false;
    }
}
