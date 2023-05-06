namespace AlgsAndDataStructures.DataStructures.OctalTree;

/// <summary>
/// Элемент восьминарного дерева
/// </summary>
/// <typeparam name="T"></typeparam>
public class OctalTreeNode<T>
{
    protected readonly int _maxNodesCount = 8;

    /// <summary>
    /// Дочерние элементы этого элемента
    /// </summary>
    public List<OctalTreeNode<T>> children = new();

    /// <summary>
    /// Родительский элемент этого элемента
    /// </summary>
    public OctalTreeNode<T>? Parent { get; set; }

    /// <summary>
    /// Значение этого элемента
    /// </summary>
    public T? Value { get; set; }

    /// <summary>
    /// Задать значение первому незанятому дочернему элементу. Ищет свободный элемент только среди ближайших дочерних элементов
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    /// <returns>Ссылка на элемент, которому присвоилось значение</returns>
    public OctalTreeNode<T> SetNewNode(T value)
    {
        if (children.Count >= _maxNodesCount)
        {
            throw new IndexOutOfRangeException("У элемента дерева нет свободного члена");
        }

        OctalTreeNode<T> newNode = new() { Value = value, Parent = this };
        children.Add(newNode);

        return newNode;
    }

    public OctalTreeNode<T>? FirstNotFreeNode() => children?.FirstOrDefault(child => child is not null);

    /// <summary>
    /// Узнать, является ли элемент потомком этого элемента. Производится глубокая проверка (через поколения)
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool IsDescendentNode(OctalTreeNode<T> node)
    {
        if (children.Any(child => child == node))
        {
            return true;
        }

        foreach(var child in children.Where(child => child is not null))
        {
            if (child.IsDescendentNode(node))
            {
                return true;
            }
        }

        return false;
    }
}
