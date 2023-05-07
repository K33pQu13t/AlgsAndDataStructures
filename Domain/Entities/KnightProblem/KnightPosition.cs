using System.Drawing;

namespace AlgsAndDataStructures.Domain.Entities.KnightProblem;

/// <summary>
/// Сущность, описывающая возможные ходы коня на шахматной доске, с историей ходов
/// </summary>
public class KnightPosition
{
    /// <summary>
    /// Текущая позиция коня
    /// </summary>
    public Point CurrentPosition { get; set; }

    /// <summary>
    /// История позиций предыдущих ходов
    /// </summary>
    public List<Point> HistoryPositions { get; set; } = new();

    /// <summary>
    /// Возможные ходы из текущей позиции
    /// </summary>
    public List<Point> PossiblePositions { get; set; } = new();
}
