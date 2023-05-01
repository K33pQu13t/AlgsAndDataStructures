using AlgsAndDataStructures.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AlgsAndDataStructures.View;

/// <summary>
/// Представление 
/// </summary>
public class SortingView : BaseConsoleView
{
    private readonly INumbersStorageService _numbersStorageService;
    private readonly IPerfomanceProviderService _perfomanceProviderService;
    private readonly ISortingService<int> _sortingService;

    public SortingView(IServiceProvider serviceProvider)
    {
        _numbersStorageService = serviceProvider.GetRequiredService<INumbersStorageService>();
        _perfomanceProviderService = serviceProvider.GetRequiredService<IPerfomanceProviderService>();
        _sortingService = serviceProvider.GetRequiredService<ISortingService<int>>();

        _options = new Dictionary<string, string>()
        {
            { "1", "Сортировка глобального массива пузырьком" },
            { "2", "Сортировка глобального массива пузырьком с контролем наличия обменов"},
            { "3", "Сортировка глобального массива перемешиванием"},
            { "4", "Сортировка глобального массива перемешиванием с контролем наличия обменов"},
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
                IEnumerable<int> numbers = _numbersStorageService.Get();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _sortingService.BubbleSort(numbers), out object? objectResult
                );
                IEnumerable<int> sortedNumbers = new List<int>();
                if (objectResult is not null)
                {
                    sortedNumbers = (IEnumerable<int>)objectResult;
                    Console.WriteLine(string.Join("\n", sortedNumbers));
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "2":
            {
                PrintOperationNameByKey(input);
                IEnumerable<int> numbers = _numbersStorageService.Get();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _sortingService.BubbleSortWithSwapChecks(numbers), out object? objectResult
                );
                IEnumerable<int> sortedNumbers = new List<int>();
                if (objectResult is not null)
                {
                    sortedNumbers = (IEnumerable<int>)objectResult;
                    Console.WriteLine(string.Join("\n", sortedNumbers));
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "3":
            {
                PrintOperationNameByKey(input);
                IEnumerable<int> numbers = _numbersStorageService.Get();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _sortingService.ShufflingSort(numbers), out object? objectResult
                );
                IEnumerable<int> sortedNumbers = new List<int>();
                if (objectResult is not null)
                {
                    sortedNumbers = (IEnumerable<int>)objectResult;
                    Console.WriteLine(string.Join("\n", sortedNumbers));
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
                }
            case "4":
            {
                PrintOperationNameByKey(input);
                IEnumerable<int> numbers = _numbersStorageService.Get();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _sortingService.ShufflingSortWithSwapChecks(numbers), out object? objectResult
                );
                IEnumerable<int> sortedNumbers = new List<int>();
                if (objectResult is not null)
                {
                    sortedNumbers = (IEnumerable<int>)objectResult;
                    Console.WriteLine(string.Join("\n", sortedNumbers));
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

        // Новая строка после каждого обработанного запроса
        Console.WriteLine();
    }
}
