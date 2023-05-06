namespace AlgsAndDataStructures.DataStructures.Tree;

/// <summary>
/// Не бинарное дерево дерево, каждый элемент которого может иметь не более фиксированного количество элементов
/// </summary>
public class NonBinaryTree<T>
{
    /// <summary>
    /// Корневой элемент дерева
    /// </summary>
    protected NonBinaryTreeNode<T>? _rootNode;

    /// <summary>
    /// Максимальное количество дочерних элементов каждого элемента
    /// </summary>
    protected readonly int _dimensions;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dimension">Максимальное количество дочерних элементов каждого элемента</param>
    public NonBinaryTree(int dimension)
    {
        _dimensions = dimension;
    }

    public NonBinaryTreeNode<T> AddToTree(T item)
    {
        if (_rootNode is null)
        {
            _rootNode = new(_dimensions) { Value = item };
            return _rootNode;
        }

        return AddToTreeInternal(_rootNode, item);
    }

    protected NonBinaryTreeNode<T> AddToTreeInternal(NonBinaryTreeNode<T> node, T item)
    {
        try
        {
            return node.SetNewNode(item);
        }
        catch
        {
            var firstNotFreeNode = node.FirstNotFreeNode();
            return firstNotFreeNode is null
                ? throw new ArgumentOutOfRangeException(nameof(node), "У дерева нет свободного элемента")
                : AddToTreeInternal(firstNotFreeNode, item);
        }
    }

    //public bool IsNodeOfThisTree(OctalTreeNode<T> node)
    //{
    //    if (_firstNode == node)
    //    {
    //        return true;
    //    }

    //    foreach(var childNode in _firstNode.children)
    //    {
    //        if (childNode.IsDescendentNode(node))
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    /// <summary>
    /// Получить коллекцию коллекций всех возможных обходов дерева
    /// </summary>
    /// <returns>Коллекция коллекций всех возможных обходов дерева</returns>
    public IEnumerable<IEnumerable<T?>> AsEnumerableOfEnumerables()
    {
        List<T?> chain = new();
        List<List<T?>> result = new();
        Traverse(_rootNode, chain, result);
        return result;
    }

    /// <summary>
    /// Обход дерева начиная с указанного элемента
    /// </summary>
    /// <param name="node"></param>
    /// <param name="chain"></param>
    /// <param name="result"></param>
    protected void Traverse(NonBinaryTreeNode<T>? node, List<T?> chain, List<List<T?>> result)
    {
        if (node is null)
        {
            return;
        }

        chain.Add(node.Value);

        if (node.children.Count == 0)
        {
            result.Add(new List<T?>(chain));
        }
        else
        {
            foreach (var child in node.children)
            {
                Traverse(child, chain, result);
            }
        }

        chain.RemoveAt(chain.Count - 1);
    }
}
