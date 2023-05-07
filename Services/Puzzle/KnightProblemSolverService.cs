using AlgsAndDataStructures.DataStructures.Tree;
using AlgsAndDataStructures.Domain.Entities.KnightProblem;
using AlgsAndDataStructures.Extensions;
using System.Drawing;

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
    public NonBinaryTree<KnightPosition> Solve(int widthOfDesk, int heightOfDesk, Point startPoint);

    /// <summary>
    /// Вывести на экран все решения
    /// </summary>
    /// <param name="solveTree"></param>
    public void PrintAllSolutions(NonBinaryTree<KnightPosition> solveTree, int widthOfDesk, int heightOfDesk);
}

public class KnightProblemSolverService : IKnightProblemSolverService
{
    protected const int knightMaxCountOfPossibleMoves = 8;
    public NonBinaryTree<KnightPosition> Solve(int widthOfDesk, int heightOfDesk, Point startPoint)
    {
        List<Point> visitedPoints = new() { startPoint };

        NonBinaryTree<KnightPosition> NonBinaryTree = new(knightMaxCountOfPossibleMoves);
        List<Point> possibleNextPoints = GetListOfPossibleMovesFromPoint(widthOfDesk, heightOfDesk, startPoint, visitedPoints);

        KnightPosition knightPosition = new()
        {
            CurrentPosition = startPoint,
            HistoryPositions = visitedPoints,
            PossiblePositions = possibleNextPoints
        };
        NonBinaryTreeNode<KnightPosition> node = NonBinaryTree.AddToTree(knightPosition);
        foreach (Point pointToAdd in possibleNextPoints)
        {
            SolveInternal(widthOfDesk, heightOfDesk, NonBinaryTree, node, pointToAdd, new(knightPosition.HistoryPositions));
        }

        return NonBinaryTree;
    }

    protected void SolveInternal(int widthOfDesk, int heightOfDesk, 
        NonBinaryTree<KnightPosition> tree, NonBinaryTreeNode<KnightPosition> node, 
        Point pointToAdd, List<Point> prohibitedPoints)
    {
        prohibitedPoints.Add(pointToAdd);
        List<Point> possiblePoints = GetListOfPossibleMovesFromPoint(widthOfDesk, heightOfDesk, pointToAdd, prohibitedPoints);
        KnightPosition knightPosition = new()
        {
            CurrentPosition = pointToAdd,
            HistoryPositions = prohibitedPoints,
            PossiblePositions = possiblePoints
        };

        NonBinaryTreeNode<KnightPosition> newNode;
        try
        {
            newNode = node.SetNewNode(knightPosition);
        }
        catch
        {
            newNode = tree.AddToTree(knightPosition);
        }

        foreach (var point in possiblePoints)
        {
            var groups = prohibitedPoints.GroupBy(p => new { p.X, p.Y });
            var duplicates = groups.Where(g => g.Count() > 1).ToList();
            string result = "";
            foreach (var d in prohibitedPoints)
            {
                result += $"list.Add(new Point() {{X = {d.X}, Y + {d.Y}}});\n";
            }

            SolveInternal(widthOfDesk, heightOfDesk, tree, newNode, point, new(prohibitedPoints));
        }
    }

    /// <summary>
    /// Вывести на экран все решения задачи
    /// </summary>
    /// <param name="solveTree"></param>
    /// <param name="widthOfDesk"></param>
    /// <param name="heightOfDesk"></param>
    public void PrintAllSolutions(NonBinaryTree<KnightPosition> solveTree, int widthOfDesk, int heightOfDesk)
    {
        // длина самого большого числа в таблице
        int maxCountOfDigits = (widthOfDesk * heightOfDesk).CountOfDigits();
        /** 
         * +1 maxCountOfDigits, так как к каждому числу справа будет приписан символ "|"
         * +1 к общему результату, так как надо учесть левую границу таблицы
         * **/
        int lengthOfTopEdge = widthOfDesk * (maxCountOfDigits + 1) + 1;

        IEnumerable<IEnumerable<KnightPosition>> solutions = solveTree.AsEnumerableOfEnumerables();
        // за решение считаем только попытки, когда произошёл обход всех клеток
        solutions = solutions.Where(solution => solution.Count() == widthOfDesk * heightOfDesk);
        if (!solutions.Any())
        {
            Console.WriteLine("Нет решений");
            return;
        }

        int solutionIndex = 0;
        foreach (List<KnightPosition> solution in solutions.Cast<List<KnightPosition>>())
        {
            Console.WriteLine($"Решение #{++solutionIndex}");
            PrintSolution(solution, widthOfDesk, heightOfDesk, lengthOfTopEdge, maxCountOfDigits);
        }
    }

    private void PrintSolution(List<KnightPosition> solution, int widthOfDesk, int heightOfDesk, int lengthOfTopEdge, int maxCountOfDigits)
    {
        // отрисовать верхнюю границу таблицы
        Console.WriteLine(string.Concat(Enumerable.Repeat("-", lengthOfTopEdge)));

        for (int y = 1; y <= heightOfDesk; y++)
        {
            Console.Write("|");
            for (int x = 1; x <= widthOfDesk; x++)
            {
                // порядковый номер этого хода
                int turnOfPoint = solution.FindIndex(searchPosition =>
                    searchPosition.CurrentPosition.X == x && searchPosition.CurrentPosition.Y == y) + 1;

                int countOfSpaces = maxCountOfDigits - turnOfPoint.CountOfDigits();

                string stringOfPoint = $"{turnOfPoint}" +
                    $"{string.Concat(Enumerable.Repeat(" ", countOfSpaces))}|";

                Console.Write(stringOfPoint);
            }
            Console.WriteLine();
        }

        // отрисовать нижнюю границу таблицы
        Console.WriteLine(string.Concat(Enumerable.Repeat("-", lengthOfTopEdge)));
        Console.WriteLine();
    }

    /// <summary>
    /// Получить список позиций, куда конь может переместиться из заданной точки
    /// </summary>
    /// <param name="desk">доска</param>
    /// <param name="point">точка, из которой будут высчитываться возможные позиции</param>
    /// <param name="prohibitedPoints">точки, которые нельзя выбрать</param>
    /// <returns></returns>
    protected List<Point> GetListOfPossibleMovesFromPoint(int widthOfDesk, int heightOfDesk, 
        Point point, IEnumerable<Point> prohibitedPoints)
    {
        List<Point> possiblePoints = new();
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

    protected void AddPossiblePointIfNotProhibited(List<Point> possiblePoints, Point point, 
        int widthOfDesk, int heightOfDesk, IEnumerable<Point> prohibitedPoints)
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
