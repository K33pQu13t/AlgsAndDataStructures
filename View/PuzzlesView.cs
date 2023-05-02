using AlgsAndDataStructures.DataStructures.CustomQueue;
using AlgsAndDataStructures.Services;
using AlgsAndDataStructures.Services.Puzzle;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;

namespace AlgsAndDataStructures.View;

internal class PuzzlesView : BaseConsoleView
{
    private readonly IPerfomanceProviderService _perfomanceProviderService;
    private readonly IFibonacciSolverService _fibonacciSolverService;
    protected readonly Dictionary<string, string> _descriptions;

    public PuzzlesView(IServiceProvider serviceProvider)
    {
        _perfomanceProviderService = serviceProvider.GetRequiredService<IPerfomanceProviderService>();
        _fibonacciSolverService = serviceProvider.GetRequiredService<IFibonacciSolverService>();

        _options = new Dictionary<string, string>()
        {
            { "1", "Числа Фибоначчи" },
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
