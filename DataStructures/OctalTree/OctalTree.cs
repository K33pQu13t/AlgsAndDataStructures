using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgsAndDataStructures.DataStructures.OctalTree;

/// <summary>
/// Восьминарное дерево
/// </summary>
public class OctalTree<T>
{
    protected OctalTreeNode<T>? _firstNode;

    public OctalTreeNode<T> AddToTree(T item)
    {
        if (_firstNode is null)
        {
            _firstNode = new() { Value = item };
            return _firstNode;
        }

        return AddToTreeInternal(_firstNode, item);
    }

    protected OctalTreeNode<T> AddToTreeInternal(OctalTreeNode<T> node, T item)
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

    public bool IsNodeOfThisTree(OctalTreeNode<T> node)
    {
        if (_firstNode == node)
        {
            return true;
        }

        foreach(var childNode in _firstNode.children)
        {
            if (childNode.IsDescendentNode(node))
            {
                return true;
            }
        }

        return false;
    }
}
