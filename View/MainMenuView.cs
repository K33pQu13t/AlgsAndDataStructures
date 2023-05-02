using AlgsAndDataStructures.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AlgsAndDataStructures.View;

/// <summary>
/// Представление главного меню
/// </summary>
internal class MainMenuView : BaseConsoleView
{
    private readonly IFileService _fileService;
    private readonly INumbersStorageService _numbersStorageService;
    private readonly IPerfomanceProviderService _perfomanceProviderService;

    private readonly SortingView _sortingView;

    public MainMenuView(IServiceProvider serviceProvider)
    {
        _fileService = serviceProvider.GetRequiredService<IFileService>();
        _numbersStorageService = serviceProvider.GetRequiredService<INumbersStorageService>();
        _perfomanceProviderService = serviceProvider.GetRequiredService<IPerfomanceProviderService>();

        _sortingView = serviceProvider.GetRequiredService<SortingView>();

        _options = new Dictionary<string, string>()
        {
            { "1", "Записать в файл N случайных целых чисел" },
            { "2", "Распечатать содержимое файла"},
            { "3", "Распечатать первый элемент файла" },
            { "4", "Распечатать последний элемент файла" },
            { "5", "Записать в текстовый файл N целых чисел, используя ручной ввод" },
            { "6", "Поместить числа из файла в глобальный целочисленный динамический список" },
            { "7", "Вывести на печать числа из глобального динамического списка" },
            { "8", "Вывести на печать первый элемент из глобального динамического списка" },
            { "9", "Вывести на печать последний элемент из глобального динамического списка" },
            { "10", "Сортировка..." },
            { "00", "Выход из программы" }
        };
    }

    protected void PrintGlobalListIsEmpty()
    {
        Console.WriteLine("Глобальный динамический список пуст");
    }

    public override void Run()
    {
        PrintMenu();

        Listen();
    }

    protected override void InputResolver(string input)
    {
        switch(input)
        {
            case "1":
            {
                PrintOperationNameByKey(input);
                string filePath = AskUserForFilePath();
                int numbersCount = AskUserForNumber("Введите количество случайных чисел:");
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(() 
                    => _fileService.WriteRandomNumbers(filePath, numbersCount)
                );
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "2":
            {
                PrintOperationNameByKey(input);
                string filePath = AskUserForFilePath();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(() 
                    => _fileService.PrintFileContent(filePath)
                );
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "3":
            {
                PrintOperationNameByKey(input);
                string filePath = AskUserForFilePath();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _fileService.PrintFirstElementOfFile(filePath)
                );
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "4":
            {
                PrintOperationNameByKey(input);
                string filePath = AskUserForFilePath();
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _fileService.PrintLastElementOfFile(filePath)
                );
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "5":
            {
                PrintOperationNameByKey(input);
                string filePath = AskUserForFilePath();
                IEnumerable<int> numbers = AskUserForNumbersCollection("Введите числа для записи:");
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _fileService.WriteNumbers(filePath, separator: " ", numbers.ToArray())
                );
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "6":
            {
                PrintOperationNameByKey(input);
                string filePath = AskUserForFilePath();
                IEnumerable<int> numbers = _fileService.GetNumbersFromFile(filePath);
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _numbersStorageService.Set(numbers)
                );
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "7":
            {
                PrintOperationNameByKey(input);
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _numbersStorageService.Get(), out object? objectResult
                );
                if (objectResult is not null)
                {
                    Console.WriteLine(string.Join('\n', (IEnumerable<int>)objectResult));
                }
                else
                {
                    PrintGlobalListIsEmpty();
                }
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "8":
            {
                PrintOperationNameByKey(input);
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _numbersStorageService.GetFirst(), out object? objectResult
                );
                if (objectResult is not null)
                {
                    Console.WriteLine($"Первый элемент глобального динамического списка: {(int)objectResult}");
                }
                else
                {
                    PrintGlobalListIsEmpty();
                }
                break;
            }
            case "9":
            {
                PrintOperationNameByKey(input);
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _numbersStorageService.GetLast(), out object? objectResult
                );
                if (objectResult is not null)
                {
                    Console.WriteLine($"Последний элемент глобального динамического списка: {(int)objectResult}");
                }
                else
                {
                    PrintGlobalListIsEmpty();
                }
                break;
            }
            case "10":
            {
                _sortingView.Run();
                // Когда управление передастся из меню выше обратно сюда, нужно будет вывести текущее меню, иначе это может дизориентировать
                PrintMenu();
                break;
            }
            case "00":
            {
                Environment.Exit(0);
                return;
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
