using AlgsAndDataStructures.DataStructures.OctalTree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgsAndDataStructures.Services.Puzzle;

/// <summary>
/// Интерфейс сервиса для решения проблемы коня на шахматной доске
/// </summary>
public interface IKnightProblemSolverService
{
    /// <summary>
    /// Решить проблему обхода коня всех полей шахматной доски с указанными длинами сторон
    /// </summary>
    /// <param name="widthOfDesk">количество клеток в ширину</param>
    /// <param name="heightOfDesk">количество клеток в высоту</param>
    /// <returns>Восьминарное дерево с полным решением всех возможных комбинаций ходов</returns>
    public OctalTree<Point> Solve(int widthOfDesk, int heightOfDesk, Point startPoint);
}

public class KnightProblemSolverService : IKnightProblemSolverService
{
    public OctalTree<Point> Solve(int widthOfDesk, int heightOfDesk, Point startPoint)
    {
        List<Point> visitedPoints = new() { startPoint };

        OctalTree<Point> octalTree = new();
        OctalTreeNode<Point> node = octalTree.AddToTree(startPoint);
        List<Point> possibleNextPoints = GetListOfPossibleMovesFromPoint(widthOfDesk, heightOfDesk, startPoint, visitedPoints);
        foreach (Point pointToAdd in possibleNextPoints)
        {
            SolveInternal(widthOfDesk, heightOfDesk, octalTree, node, pointToAdd, visitedPoints);
        }

        return octalTree;
    }

    private void SolveInternal(int widthOfDesk, int heightOfDesk, OctalTree<Point> tree, OctalTreeNode<Point> node, Point pointToAdd, List<Point> prohibitedPoints)
    {
        prohibitedPoints.Add(pointToAdd);

        OctalTreeNode<Point> newNode = null;
        try
        {
            newNode = node.SetNewNode(pointToAdd);
        }
        catch
        {
            newNode = tree.AddToTree(pointToAdd);
        }
        List<Point> possiblePoints = GetListOfPossibleMovesFromPoint(widthOfDesk, heightOfDesk, pointToAdd, prohibitedPoints);
        foreach (var point in possiblePoints)
        {
            SolveInternal(widthOfDesk, heightOfDesk, tree, newNode, point, prohibitedPoints);
        }
    }

    /// <summary>
    /// Получить список позиций, куда конь может переместиться из заданной точки
    /// </summary>
    /// <param name="desk">доска</param>
    /// <param name="point">точка, из которой будут высчитываться возможные позиции</param>
    /// <param name="prohibitedPoints">точки, которые нельзя выбрать</param>
    /// <returns></returns>
    private List<Point> GetListOfPossibleMovesFromPoint(int widthOfDesk, int heightOfDesk, Point point, IEnumerable<Point> prohibitedPoints)
    {
        List<Point> possiblePoints = new List<Point>();
        Point possiblePoint = new() { X = point.X - 1, Y = point.Y + 2 };
        AddPossiblePointIfNotProhibited(possiblePoints, possiblePoint, widthOfDesk, heightOfDesk, prohibitedPoints);
        possiblePoint = new() { X = point.X + 1, Y = point.Y + 2 };
        AddPossiblePointIfNotProhibited(possiblePoints, possiblePoint, widthOfDesk, heightOfDesk, prohibitedPoints);
        possiblePoint = new() { X = point.X + 2, Y = point.Y - 1 };
        AddPossiblePointIfNotProhibited(possiblePoints, possiblePoint, widthOfDesk, heightOfDesk, prohibitedPoints);
        possiblePoint = new() { X = point.X + 2, Y = point.Y + 1 };
        AddPossiblePointIfNotProhibited(possiblePoints, possiblePoint, widthOfDesk, heightOfDesk, prohibitedPoints);
        possiblePoint = new() { X = point.X - 2, Y = point.Y - 1 };
        AddPossiblePointIfNotProhibited(possiblePoints, possiblePoint, widthOfDesk, heightOfDesk, prohibitedPoints);
        possiblePoint = new() { X = point.X - 2, Y = point.Y + 1 };
        AddPossiblePointIfNotProhibited(possiblePoints, possiblePoint, widthOfDesk, heightOfDesk, prohibitedPoints);
        possiblePoint = new() { X = point.X - 1, Y = point.Y - 2 };
        AddPossiblePointIfNotProhibited(possiblePoints, possiblePoint, widthOfDesk, heightOfDesk, prohibitedPoints);
        possiblePoint = new() { X = point.X + 1, Y = point.Y - 2 };
        AddPossiblePointIfNotProhibited(possiblePoints, possiblePoint, widthOfDesk, heightOfDesk, prohibitedPoints);

        return possiblePoints;
    }

    private void AddPossiblePointIfNotProhibited(List<Point> possiblePoints, Point point, int widthOfDesk, int heightOfDesk, IEnumerable<Point> prohibitedPoints)
    {
        if (point.X <= 0 || point.X > widthOfDesk || point.Y <= 0 || point.Y > heightOfDesk)
        {
            return;
        }
        if (!prohibitedPoints.Any(prohibitedPoint => prohibitedPoint == point))
        {
            possiblePoints.Add(point);
        }
    }
}
