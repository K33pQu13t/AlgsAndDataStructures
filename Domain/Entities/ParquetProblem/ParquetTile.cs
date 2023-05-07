using AlgsAndDataStructures.Domain.Enums.ParquetProblem;
using System.Drawing;

namespace AlgsAndDataStructures.Domain.Entities.ParquetProblem;

/// <summary>
/// Сущность, описывающая плитку для закладки области
/// </summary>
public class ParquetTile
{
    /// <summary>
    /// Длина плитки
    /// </summary>
    public static int TileLength => 2;

    /// <summary>
    /// Символ обозначения позиции, куда нельзя положить плитку
    /// </summary>
    public static char ProhibitedSymbol => ' ';

    /// <summary>
    /// Корневая позиция плитки
    /// </summary>
    public Point RootPosition { get; set; }

    /// <summary>
    /// Направление плитки
    /// </summary>
    public TileDirection TileDirection { get; set; }

    private char _symbol;

    /// <summary>
    /// Условное обозначение плитки
    /// </summary>
    public char Symbol { 
        get => _symbol; 
        set 
        {
            if (value == ProhibitedSymbol)
            {
                throw new ApplicationException($"Символ \'{ProhibitedSymbol}\' зарезервирован под места куда нельзя положить плитку, " +
                    "и не может быть использован в качестве условного обозначения плитки");
            }
            _symbol = value;
        }
    }

    /// <summary>
    /// Получить коллекцию каждого элемента плитки
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Point> GetCoveredPositions()
    {
        List<Point> coveredPositions = new() { RootPosition };
        for (int index = 0; index < TileLength - 1; index++)
        {
            Point previousPosition = coveredPositions.Last();
            Point nextPosition = TileDirection switch
            {
                TileDirection.Up => new Point(previousPosition.X, previousPosition.Y - 1),
                TileDirection.Right => new Point(previousPosition.X + 1, previousPosition.Y),
                TileDirection.Down => new Point(previousPosition.X, previousPosition.Y + 1),
                TileDirection.Left => new Point(previousPosition.X - 1, previousPosition.Y),
                _ => new Point(previousPosition.X, previousPosition.Y)
            };
            coveredPositions.Add(nextPosition);
        }

        return coveredPositions;
    }

    /// <summary>
    /// Получить коллекцию позиций каждого элемента плитки, какие были бы заняты, если бы плитка была размещена указанным образом
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Point> GetCoveredPositions(int x, int y, TileDirection direction, int areaWidth, int areaHeight)
    {
        List<Point> coveredPositions = new() { new(x, y) };
        for (int index = 0; index < TileLength - 1; index++)
        {
            Point previousPosition = coveredPositions.Any()
                ? coveredPositions.Last()
                : new(x, y);
            Point nextPosition = direction switch
            {
                TileDirection.Up => new Point(previousPosition.X, previousPosition.Y - 1),
                TileDirection.Right => new Point(previousPosition.X + 1, previousPosition.Y),
                TileDirection.Down => new Point(previousPosition.X, previousPosition.Y + 1),
                TileDirection.Left => new Point(previousPosition.X - 1, previousPosition.Y),
                _ => new Point(previousPosition.X, previousPosition.Y)
            };
            if (nextPosition.X < areaWidth && nextPosition.Y < areaHeight)
            {
                coveredPositions.Add(nextPosition);
            }
        }
        if (coveredPositions.Count == TileLength)
        {
            return coveredPositions;
        }

        return new List<Point>();
    }
}
