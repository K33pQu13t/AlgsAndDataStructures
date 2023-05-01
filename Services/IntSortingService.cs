namespace AlgsAndDataStructures.Services;

public class IntSortingService : ISortingService<int>
{
    public IEnumerable<int> BubbleSort(IEnumerable<int> data)
    {
        List<int> sortedData = new(data);
        int count = sortedData.Count;
        for (int index1 = 0; index1 < count - 1; index1++)
        {
            for (int index2 = 0; index2 < count - 1 - index1 ; index2++)
            {
                if (sortedData.ElementAt(index2) > sortedData.ElementAt(index2 + 1))
                {
                    (sortedData[index2 + 1], sortedData[index2]) = (sortedData[index2], sortedData[index2 + 1]);
                }
            }
        }
        return sortedData;
    }

    public IEnumerable<int> BubbleSortWithSwaps(IEnumerable<int> data)
    {
        List<int> sortedData = new(data);
        int count = sortedData.Count;
        bool isSwapped;
        for (int index1 = 0; index1 < count - 1; index1++)
        {
            isSwapped = false;
            for (int index2 = 0; index2 < count - 1 - index1; index2++)
            {
                if (sortedData.ElementAt(index2) > sortedData.ElementAt(index2 + 1))
                {
                    (sortedData[index2 + 1], sortedData[index2]) = (sortedData[index2], sortedData[index2 + 1]);
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
}
