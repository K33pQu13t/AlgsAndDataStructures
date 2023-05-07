using AlgsAndDataStructures.DataStructures.Tree;
using AlgsAndDataStructures.Domain.Entities.KnightProblem;
using AlgsAndDataStructures.Domain.Entities.ParquetProblem;
using AlgsAndDataStructures.Services;
using AlgsAndDataStructures.Services.Puzzle;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using System.Numerics;

namespace AlgsAndDataStructures.View;

internal class PuzzlesView : BaseConsoleView
{
    private readonly IPerfomanceProviderService _perfomanceProviderService;
    private readonly IFibonacciSolverService _fibonacciSolverService;
    private readonly INoSenceRecursionSolverService _noSenceRecursionSolverService;
    private readonly IKnightProblemSolverService _knightProblemSolverService;
    private readonly IParquetProblemSolverService _parquetProblemSolverService;

    public PuzzlesView(IServiceProvider serviceProvider)
    {
        _perfomanceProviderService = serviceProvider.GetRequiredService<IPerfomanceProviderService>();
        _fibonacciSolverService = serviceProvider.GetRequiredService<IFibonacciSolverService>();
        _noSenceRecursionSolverService = serviceProvider.GetRequiredService<INoSenceRecursionSolverService>();
        _knightProblemSolverService = serviceProvider.GetRequiredService<IKnightProblemSolverService>();
        _parquetProblemSolverService = serviceProvider.GetRequiredService<IParquetProblemSolverService>();

        _options = new Dictionary<string, string>()
        {
            { "1", "Числа Фибоначчи" },
            { "2", "Бессмысленная рекурсивная функция" },
            { "3", "Задача о ходе коня" },
            { "4", "Паркет" },
            { "99", "Назад" }
        };
    }

    public override void Run()
    {
        Console.WriteLine();
        PrintMenu();

        Listen();
    }

    protected override void InputResolver(string input)
    {
        switch (input)
        {
            case "1":
            {
                PrintOperationNameByKey(input);
                int countOfIterations = AskUserForNumber("Введите количество итераций алгоритма Фибоначчи: ");
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _fibonacciSolverService.Solve(countOfIterations), out object? objectResult
                );
                if (objectResult is not null)
                {
                    BigInteger result = (BigInteger)objectResult;
                    Console.WriteLine($"Число, которое получится после {countOfIterations} итераций алгоритма Фибоначчи: {result}");
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "2":
            {
                PrintOperationNameByKey(input);
                int a = AskUserForNumber("Введите a: ");
                int c = AskUserForNumber("Введите c: ");
                int m = AskUserForNumber("Введите m: ");
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _noSenceRecursionSolverService.NoSenceRecursion(a, c, m), out object? objectResult
                );
                if (objectResult is not null)
                {
                    decimal result = (decimal)objectResult;
                    Console.WriteLine($"Результат этой рекурсивной функции: {result}");
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "3":
            {
                PrintOperationNameByKey(input);
                int widthOfDesk = AskUserForNumber("Введите ширину доски: ");
                int heightOfDesk = AskUserForNumber("Введите высоту доски: ");
                int x = AskUserForNumber("Введите стартовую точку по X");
                int y = AskUserForNumber("Введите стартовую точку по Y");
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _knightProblemSolverService.Solve(widthOfDesk, heightOfDesk, new Point() { X = x, Y = y}), 
                            out object? objectResult
                );
                if (objectResult is not null)
                {
                    NonBinaryTree<KnightPosition> result = (NonBinaryTree<KnightPosition>)objectResult;
                    _knightProblemSolverService.PrintAllSolutions(result, widthOfDesk, heightOfDesk);
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "4":
            {
                PrintOperationNameByKey(input);
                List<string> parquetMaket = AskUserForStringList("" +
                    "Вставьте макет области, которую необходимо покрыть плиткой.\n" +
                    "Используйте любой символ, кроме пробела, для обозначения места, куда можно положить плитку.\n" +
                    "Используйте символ пробела для обозначения места, куда положить плитку нельзя.\n" +
                    "Если одна из строк будет короче остальных, " +
                    "она автоматически заполнится пробелами до конца оставшегося места.\n" +
                    "Вводите макет по одной строчке. Нажмите enter два раза, когда закончите. " +
                    "Пример макета:\n" +
                    "aaaaaaa\n" +
                    "aaaa aa\n" +
                    "a aaaaa\n" +
                    "aaa a a\n"
                );

                int maxLineLength = parquetMaket.Max(line => line.Length);

                // найти все позиции, куда нельзя класть плитку
                List<Point> prohibitedPositions = new();
                for (int lineIndex = 0; lineIndex < parquetMaket.Count; lineIndex ++)
                {
                    if (parquetMaket[lineIndex].Length < maxLineLength)
                    {
                        parquetMaket[lineIndex] += string.Concat(Enumerable.Repeat(' ', maxLineLength - parquetMaket[lineIndex].Length));
                    }
                    for (int charIndex = 0; charIndex < parquetMaket[lineIndex].Length; charIndex++)
                    {
                        if (parquetMaket[lineIndex][charIndex] == ' ')
                        {
                            prohibitedPositions.Add(new Point(x: charIndex, y: lineIndex));
                        }
                    }
                }

                try
                {
                    long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _parquetProblemSolverService.Solve(maxLineLength, parquetMaket.Count, prohibitedPositions),
                        out object? objectResult
                    );
                    if (objectResult is not null)
                    {
                        ParquetArea result = (ParquetArea)objectResult;
                        Console.WriteLine("План укладки паркета: ");
                        _parquetProblemSolverService.PrintParquet(result);
                    }
                    PrintSuccess();
                    PrintHowMuchMillisecondsHavePassed(milliseconds);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                break;
            }
            case "99":
            {
                StopListening();
                break;
            }

            default:
            {
                PrintNoCommandRecognized();
                break;
            }
        }
    }
}
