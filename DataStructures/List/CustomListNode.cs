namespace AlgsAndDataStructures.DataStructures.List;

/// <summary>
/// Элемент обобщённого кастомного списка
/// </summary>
/// <typeparam name="T"></typeparam>
public class CustomListNode<T>
{
    /// <summary>
    /// Значение элемента
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Ссылка на следующий элемент
    /// </summary>
    public CustomListNode<T> Next { get; set; }
}
