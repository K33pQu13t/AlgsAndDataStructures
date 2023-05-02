using AlgsAndDataStructures.DataStructures.CustomQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlgsAndDataStructures.Extensions;

public static class CustomListExtentions
{
    public static CustomList<T> BubbleSort<T>(this CustomList<T> customList)
    {
        Comparer<T> comparer = Comparer<T>.Default;
        CustomList<T> sortedData = new(customList);
        int count = sortedData.Count;
        for (int index1 = 0; index1 < count - 1; index1++)
        {
            for (int index2 = 0; index2 < count - 1 - index1; index2++)
            {
                if (comparer.Compare(sortedData[index2], sortedData[index2 + 1]) > 0)
                {
                    (sortedData[index2], sortedData[index2 + 1]) = (sortedData[index2 + 1], sortedData[index2]);
                }
            }
        }
        return sortedData;
    }

    /// <summary>
    /// Отсортировать коллекцию гномьей сортировкой
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="customList"></param>
    /// <returns>Новая, отсортированная коллекция</returns>
    public static CustomList<T> GnomeSort<T>(this CustomList<T> customList)
    {
        Comparer<T> comparer = Comparer<T>.Default;
        CustomList<T> sortedData = new(customList);
        int index = 0;
        while (index < sortedData.Count)
        {
            if (index == 0 || comparer.Compare(sortedData[index - 1], sortedData[index]) < 0)
            {
                index++;
            }
            else
            {
                if (comparer.Compare(sortedData[index - 1], sortedData[index]) > 0)
                {
                    (sortedData[index - 1], sortedData[index]) = (sortedData[index], sortedData[index - 1]);
                }
                index--;
            }
        }
        return sortedData;
    }
}
