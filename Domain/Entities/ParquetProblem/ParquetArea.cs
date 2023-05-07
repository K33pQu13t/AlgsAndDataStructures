using AlgsAndDataStructures.Domain.Enums.ParquetProblem;
using System.Drawing;

namespace AlgsAndDataStructures.Domain.Entities.ParquetProblem
{
    /// <summary>
    /// Сущность, описывающая область для закладки плиткой
    /// </summary>
    public class ParquetArea
    {
        /// <summary>
        /// Ширина области
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота области
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Места, в которых нельзя положить плитку
        /// </summary>
        public IEnumerable<Point> ProhibitedPositions { get; init; } = new List<Point>();

        /// <summary>
        /// Места, в которых расположена плитка
        /// </summary>
        public List<ParquetTile> Tiles { get; set; } = new();

        /// <summary>
        /// Узнать, доступна ли позиция для укладки плитки. 
        /// Позиция доступна, если не входит в список запрещённых позиций, 
        /// и если там ещё не лежит другая плитка
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true, если позиция доступна</returns>
        public bool IsPositionFree(int x, int y)
        {
            return !ProhibitedPositions.Any(point => point.X == x && point.Y == y) 
                && !Tiles.Any(tile => tile.GetCoveredPositions()
                    .Any(position => position.X == x && position.Y == y));
        }

        /// <summary>
        /// Узнать, доступна ли позиция для укладки плитки. 
        /// Позиция доступна, если не входит в список запрещённых позиций, 
        /// и если там ещё не лежит другая плитка
        /// </summary>
        /// <param name="tileRootPosition">Позиция корня плитки</param>
        /// <returns>true, если позиция доступна</returns>
        public bool IsPositionFree(Point tileRootPosition)
        {
            return IsPositionFree(tileRootPosition.X, tileRootPosition.Y);
        }

        public bool IsPositionFreeToPlace(int x, int y, TileDirection direction)
        {
            IEnumerable<Point> positions = ParquetTile.GetCoveredPositions(x, y, direction, Width, Height);
            return positions.Count() > 0 && positions.All(position => IsPositionFree(position));
        }
    }
}
