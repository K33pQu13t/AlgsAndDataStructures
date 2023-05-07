using AlgsAndDataStructures.Domain.Entities.ParquetProblem;
using AlgsAndDataStructures.Domain.Enums.ParquetProblem;
using System.Drawing;

namespace AlgsAndDataStructures.Services.Puzzle;

public interface IParquetProblemSolverService
{
    ParquetArea Solve(int areaWidth, int areaHeight, IEnumerable<Point> prohibitedPositions);

    void PrintParquet(ParquetArea parquetArea);
}

public class ParquetProblemSolverService : IParquetProblemSolverService
{
    public ParquetArea Solve(int areaWidth, int areaHeight, IEnumerable<Point> prohibitedPositions)
    {
        ParquetArea area = new() { Width = areaWidth, Height = areaHeight, ProhibitedPositions = prohibitedPositions};
        AnalyticCheckIfItIsPossibleToSolve(areaWidth, areaHeight, prohibitedPositions);

        char nextSymbol = 'A';

        for (int y = 0; y < areaHeight; y++)
        {
            for (int x = 0; x < areaWidth; x++)
            {
                if (area.IsPositionFreeToPlace(x, y, TileDirection.Right))
                {
                    area.Tiles.Add(new() 
                    { 
                        RootPosition = new(x, y),
                        TileDirection = TileDirection.Right, 
                        Symbol = nextSymbol++
                    });
                }
                else if (area.IsPositionFreeToPlace(x, y, TileDirection.Down))
                {
                    area.Tiles.Add(new() 
                    { 
                        RootPosition = new(x, y),
                        TileDirection = TileDirection.Down, 
                        Symbol = nextSymbol++
                    });
                }
            }
        }

        if (area.Tiles.Count * ParquetTile.TileLength != (area.Width * area.Height - area.ProhibitedPositions.Count()))
        {
            throw GetImpossibleException();
        }

        return area;
    }

    /// <summary>
    /// Вывести паркет в консоль
    /// </summary>
    /// <param name="parquetArea"></param>
    public void PrintParquet(ParquetArea parquetArea)
    {
        for (int y = 0; y < parquetArea.Height; y++)
        {
            for (int x = 0; x < parquetArea.Width; x++)
            {
                // если позиция входит в список запрещённых
                if (parquetArea.ProhibitedPositions.Any(point => point.X == x && point.Y == y))
                {
                    Console.Write(' ');
                    continue;
                }
                ParquetTile tile = parquetArea.Tiles.Find(parquetTiles => parquetTiles.GetCoveredPositions()
                    .Any(parquetTile => parquetTile.X == x && parquetTile.Y == y));
                Console.Write(tile.Symbol);
            }
            Console.WriteLine();
        }
    }

    protected void AnalyticCheckIfItIsPossibleToSolve(int areaWidth, int areaHeight, IEnumerable<Point> prohibitedPositions)
    {
        if ((areaWidth * areaHeight - prohibitedPositions.Count()) % ParquetTile.TileLength is not 0)
        {
            throw GetImpossibleException();
        }
    }

    /// <summary>
    /// Получить объект ошибки о невозможности уложить плитку по описанным условиям
    /// </summary>
    /// <returns></returns>
    private Exception GetImpossibleException() => new ApplicationException($"Эту область невозможно покрыть плитками размером {ParquetTile.TileLength}");
}
