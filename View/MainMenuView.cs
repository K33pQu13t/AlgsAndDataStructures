using AlgsAndDataStructures.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AlgsAndDataStructures.View;

/// <summary>
/// Представление главного меню
/// </summary>
internal class MainMenuView : IConsoleView
{
    private readonly IFileService _fileService;
    private readonly INumbersStorageService _numbersStorageService;
    private readonly IPerfomanceProviderService _perfomanceProviderService;

    private readonly Dictionary<int, string> _options = new Dictionary<int, string>()
    {
        { 1, "Записать в файл N случайных целых чисел" },
        { 2, "Распечатать содержимое файла"},
        { 3, "Распечатать первый элемент файла" },
        { 4, "Распечатать последний элемент файла" },
        { 5, "Записать в текстовый файл N целых чисел, используя ручной ввод" },
        { 6, "Поместить числа из файла в глобальный целочисленный динамический массив" },
        { 7, "Вывести на печать числа из глобального динамического массива" },
        { 8, "Выход из программы" }
    };

    public MainMenuView(IServiceProvider serviceProvider)
    {
        _fileService = serviceProvider.GetRequiredService<IFileService>();
        _numbersStorageService = serviceProvider.GetRequiredService<INumbersStorageService>();
        _perfomanceProviderService = serviceProvider.GetRequiredService<IPerfomanceProviderService>();
    }

    #region Принтеры
    /// <summary>
    /// Вывести в консоль меню команд
    /// </summary>
    private void PrintMenu()
    {
        _options.ToList().ForEach(option => Console.WriteLine($"{option.Key}) {option.Value}"));
    }

    /// <summary>
    /// Вывести наименование операции по её ключу
    /// </summary>
    /// <param name="key"></param>
    private void PrintOperationNameByKey(int key)
    {
        Console.WriteLine($"Операция: {_options.GetValueOrDefault(key)}");
    }

    /// <summary>
    /// Вывести сообщение об успехе
    /// </summary>
    private void PrintSuccess()
    {
        Console.WriteLine("Операция успешно завершена");
    }

    /// <summary>
    /// Вывести сообщение о прошедших с вызова метода миллисекунд
    /// </summary>
    /// <param name="milliseconds"></param>
    private void PrintHowMuchMillisecondsHavePassed(long milliseconds)
    {
        string ending = string.Empty;
        var lastDigit = milliseconds % 10;
        if (lastDigit == 0)
        {
            ending = string.Empty;
        }
        else if (lastDigit == 1)
        {
            ending = "у";
        }
        else if (lastDigit <= 4) 
        {
            ending = "ы";
        }

        Console.WriteLine($"Это заняло {milliseconds} милисекунд{ending}");
    }
    #endregion

    public void Run()
    {
        PrintMenu();

        Listen();
    }

    /// <summary>
    /// Ожидать ввода от пользователя
    /// </summary>
    private void Listen()
    {
        Console.WriteLine("Введите команду: ");
        string input = Console.ReadLine();
        InputResolver(input);

        Listen();
    }

    /// <summary>
    /// Найти необходимое действие в соответствии с командой
    /// </summary>
    /// <param name="input"></param>
    private void InputResolver(string input)
    {
        switch(input)
        {
            case "1":
            {
                PrintOperationNameByKey(int.Parse(input));
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
                PrintOperationNameByKey(int.Parse(input));
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
                PrintOperationNameByKey(int.Parse(input));
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
                PrintOperationNameByKey(int.Parse(input));
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
                PrintOperationNameByKey(int.Parse(input));
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
                PrintOperationNameByKey(int.Parse(input));
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
                PrintOperationNameByKey(int.Parse(input));
                long milliseconds = _perfomanceProviderService.RunToCheckPerfomance(()
                    => _numbersStorageService.Get(), out object? objectResult
                );
                IEnumerable<int> numbers = new List<int>();
                if (objectResult is not null)
                {
                    numbers = (IEnumerable<int>)objectResult;
                }
                Console.WriteLine(string.Join('\n', numbers));
                PrintSuccess();
                PrintHowMuchMillisecondsHavePassed(milliseconds);
                break;
            }
            case "8":
            {
                Environment.Exit(0);
                return;
            }

            default:
            {
                Console.WriteLine("Неизвестная команда. Введите один из номеров ниже:");
                PrintMenu();
                break;
            }
        }

        // Новая строка после каждого обработанного запроса
        Console.WriteLine();
    }

    /// <summary>
    /// Получить от пользователя целочисленное число
    /// </summary>
    /// <param name="askingMessage"></param>
    /// <returns></returns>
    private int AskUserForNumber(string askingMessage)
    {
        Console.WriteLine(askingMessage);
        return int.Parse(Console.ReadLine());
    }

    /// <summary>
    /// Получить от пользователя коллекцию чисел, вводимых по нажатию enter
    /// </summary>
    /// <param name="askingMessage"></param>
    /// <returns></returns>
    private IEnumerable<int> AskUserForNumbersCollection(string askingMessage)
    {
        Console.WriteLine($"{askingMessage}" +
            $"\n(Вводите числа нажатием Enter. Нажмите Enter два раза подряд, чтобы закончить)");
        List<int> numbers = new List<int>();
        string? input = Console.ReadLine();
        while (input != null && !string.IsNullOrEmpty(input))
        {
            numbers.Add(int.Parse(input));
            input = Console.ReadLine();
        }
        return numbers;
    }

    /// <summary>
    /// Получить от пользователя путь до файла
    /// </summary>
    /// <returns></returns>
    private string AskUserForFilePath(string? askingMessage = null)
    {
        Console.WriteLine(askingMessage ?? "Введите путь до файла: ");
        string filePath = Console.ReadLine();
        if (filePath.StartsWith('"') && filePath.EndsWith('"'))
        {
            // Обрезать крайние кавычки
            return filePath[1..^1];
        }
        return filePath;
    }
}
