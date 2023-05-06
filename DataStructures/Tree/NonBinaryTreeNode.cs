namespace AlgsAndDataStructures.DataStructures.Tree;

/// <summary>
/// Элемент небинарного дерева
/// </summary>
/// <typeparam name="T"></typeparam>
public class NonBinaryTreeNode<T>
{
    protected readonly int _maxNodesCount;

    /// <summary>
    /// Дочерние элементы этого элемента
    /// </summary>
    public List<NonBinaryTreeNode<T>> children = new();

    /// <summary>
    /// Родительский элемент этого элемента
    /// </summary>
    public NonBinaryTreeNode<T>? Parent { get; set; }

    /// <summary>
    /// Значение этого элемента
    /// </summary>
    public T? Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dimensions">Максимальное количество дочерних элементов</param>
    public NonBinaryTreeNode(int dimensions)
    {
        _maxNodesCount = dimensions;
    }

    /// <summary>
    /// Задать значение первому незанятому дочернему элементу. Ищет свободный элемент только среди ближайших дочерних элементов
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    /// <returns>Ссылка на элемент, которому присвоилось значение</returns>
    public NonBinaryTreeNode<T> SetNewNode(T value)
    {
        if (children.Count >= _maxNodesCount)
        {
            throw new IndexOutOfRangeException("У элемента дерева нет места для нового элемента");
        }

        NonBinaryTreeNode<T> newNode = new(_maxNodesCount) { Value = value, Parent = this };
        children.Add(newNode);

        return newNode;
    }

    public NonBinaryTreeNode<T>? FirstNotFreeNode() => children?.FirstOrDefault(child => child is not null);

    /// <summary>
    /// Получить глубину уровня текущего элемента в дереве
    /// </summary>
    /// <returns></returns>
    public int GetDepthOfCurrentNode()
    {
        int depth = 1;
        NonBinaryTreeNode<T> currentNode = this;
        while (currentNode.Parent is not null) 
        {
            depth++;
            currentNode = currentNode.Parent;
        }
        return depth;
    }
}
