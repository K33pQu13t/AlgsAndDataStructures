using AlgsAndDataStructures.DataStructures.List;
using AlgsAndDataStructures.Extensions;
using AlgsAndDataStructures.Services;
using AlgsAndDataStructures.Services.Sorting;
using Microsoft.Extensions.DependencyInjection;

namespace AlgsAndDataStructures.View;

/// <summary>
/// Представление 
/// </summary>
internal class SortingView : BaseConsoleView
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
            { "1", "Сортировка глобального списка пузырьком" },
            { "2", "Сортировка глобального списка пузырьком с контролем наличия обменов" },
            { "3", "Сортировка глобального списка перемешиванием" },
            { "4", "Сортировка глобального списка перемешиванием с контролем наличия обменов" },
            { "5", "Сортировка глобального списка вставками" },
            { "6", "Гномья сортировка глобального списка" },
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
                CustomList<int> customList = _numbersStorageService.GetAsCustomList();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => customList.BubbleSort(), out object? objectResult
                );
                if (objectResult is not null)
                {
                    CustomList<int> sortedCustomList = (CustomList<int>)objectResult;
                    Console.WriteLine(string.Join("\n", sortedCustomList.AsEnumerable()));
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
                if (objectResult is not null)
                {
                    Console.WriteLine(string.Join("\n", (IEnumerable<int>)objectResult));
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
                if (objectResult is not null)
                {
                    Console.WriteLine(string.Join("\n", (IEnumerable<int>)objectResult));
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
                if (objectResult is not null)
                {
                    Console.WriteLine(string.Join("\n", (IEnumerable<int>)objectResult));
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "5":
            {
                PrintOperationNameByKey(input);
                IEnumerable<int> numbers = _numbersStorageService.Get();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _sortingService.InsertionSort(numbers), out object? objectResult
                );
                if (objectResult is not null)
                {
                    Console.WriteLine(string.Join("\n", (IEnumerable<int>)objectResult));
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "6":
            {
                PrintOperationNameByKey(input);
                CustomList<int> customList = _numbersStorageService.GetAsCustomList();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => customList.GnomeSort(), out object? objectResult
                );
                if (objectResult is not null)
                {
                    CustomList<int> sortedCustomList = (CustomList<int>)objectResult;
                    Console.WriteLine(string.Join("\n", sortedCustomList.AsEnumerable()));
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
