namespace AlgsAndDataStructures.DataStructures.CustomQueue;

/// <summary>
/// Обобщённый кастомный список
/// </summary>
/// <typeparam name="T"></typeparam>
public class CustomList<T>
{
    protected CustomListNode<T>? first;

    public CustomList() { }

    public CustomList(IEnumerable<T> collection)
    {
        foreach(var item in collection)
        {
            Add(item);
        }
    }

    public CustomList(CustomList<T> customList)
    {
        for (int index = 0; index < customList.Count; index++)
        {
            Add(customList[index]);
        }
    }

    /// <summary>
    /// Получить значение первого элемента списка
    /// </summary>
    /// <returns>значение либо null при отсутствии элементов</returns>
    public T? First()
    {
        if (first == null)
        {
            return default;
        }

        return first.Value;
    }

    /// <summary>
    /// Получить значение последнего элемента списка
    /// </summary>
    /// <returns>значение либо null при отсутствии элементов</returns>
    public T? Last()
    {
        var lastNode = LastNode();
        if (lastNode == null)
        {
            return default;
        }
        return lastNode.Value;
    }

    /// <summary>
    /// Получить последний элемент списка
    /// </summary>
    /// <returns>Последний элемент либо null при отсутствии элементов</returns>
    protected CustomListNode<T>? LastNode()
    {
        var last = first;
        while (last?.Next != null)
        {
            last = last.Next;
        }
        return last;
    }

    /// <summary>
    /// Добавить в конец списка значение
    /// </summary>
    /// <param name="value">значение для добавления</param>
    public void Add(T value)
    {
        if (first == null)
        {
            first = new CustomListNode<T> { Value = value };
            return;
        }
        LastNode().Next = new CustomListNode<T> { Value = value };
    }

    /// <summary>
    /// Представить список в виде <see cref="IEnumerable{T}"/> коллекции
    /// </summary>
    /// <returns>Коллекция либо пустая коллекция, если список пуст</returns>
    public IEnumerable<T> AsEnumerable()
    {
        List<T> result = new();
        var last = first;
        while (last != null)
        {
            result.Add(last.Value);
            last = last.Next;
        }

        return result;
    }

    /// <summary>
    /// Количество элементов в списке
    /// </summary>
    /// <returns></returns>
    public int Count => AsEnumerable().Count();

    public T this[int index]
    {
        get
        {
            var targetNode = first ?? throw new IndexOutOfRangeException();
            for (int elementIndex = 0; elementIndex < index; elementIndex++)
            {
                targetNode = targetNode.Next;
                if (targetNode == null)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            return targetNode.Value;
        }
        set
        {
            var targetNode = first ?? throw new IndexOutOfRangeException();
            for (int elementIndex = 0; elementIndex < index; elementIndex++)
            {
                targetNode = targetNode.Next;
                if (targetNode == null)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            targetNode.Value = value;
        }
    }
}
