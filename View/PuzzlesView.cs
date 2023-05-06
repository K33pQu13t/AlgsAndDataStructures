using AlgsAndDataStructures.DataStructures.CustomQueue;
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
    protected readonly Dictionary<string, string> _descriptions;

    public PuzzlesView(IServiceProvider serviceProvider)
    {
        _perfomanceProviderService = serviceProvider.GetRequiredService<IPerfomanceProviderService>();
        _fibonacciSolverService = serviceProvider.GetRequiredService<IFibonacciSolverService>();
        _noSenceRecursionSolverService = serviceProvider.GetRequiredService<INoSenceRecursionSolverService>();
        _knightProblemSolverService = serviceProvider.GetRequiredService<IKnightProblemSolverService>();

        _options = new Dictionary<string, string>()
        {
            { "1", "Числа Фибоначчи" },
            { "2", "Бессмысленная рекурсивная функция" },
            { "3", "Задача о ходе коня" },
            { "99", "Назад" }
        };

        _descriptions = new Dictionary<string, string>()
        {
            { "1", "Числа Фибоначчи" },
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
                //int widthOfDesk = AskUserForNumber("Введите ширину доски: ");
                //int heightOfDesk = AskUserForNumber("Введите высоту доски: ");
                //int x = AskUserForNumber("Введите стартовую точку по X");
                //int y = AskUserForNumber("Введите стартовую точку по Y");
                int widthOfDesk = 5;
                int heightOfDesk = 5;
                int x = 3;
                int y = 3;
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _knightProblemSolverService.Solve(widthOfDesk, heightOfDesk, new Point() { X = x, Y = y}), 
                            out object? objectResult
                );
                //if (objectResult is not null)
                //{
                //    BigInteger result = (BigInteger)objectResult;
                //    Console.WriteLine($"Число, которое получится после {countOfIterations} итераций алгоритма Фибоначчи: {result}");
                //}
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
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
