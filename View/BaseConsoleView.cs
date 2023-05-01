namespace AlgsAndDataStructures.View;

/// <summary>
/// Базовое консольное представление
/// </summary>
public abstract class BaseConsoleView : IConsoleView
{
    protected virtual Dictionary<string, string> _options { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// true если была команда выхода из представления
    /// </summary>
    private bool _isExeting = false;

    public abstract void Run();

    /// <summary>
    /// Продолжить принимать ввод команд пользователя
    /// </summary>
    private void ContinueListening()
    {
        _isExeting = false;
    }

    /// <summary>
    /// Перестать ожидать ввод команд пользователя
    /// </summary>
    protected void StopListening()
    {
        _isExeting = true;
    }

    /// <summary>
    /// Ожидать ввода от пользователя
    /// </summary>
    protected virtual void Listen()
    {
        if (_isExeting)
        {
            ContinueListening();
            return;
        }

        Console.WriteLine("Введите команду: ");
        string input = Console.ReadLine();
        InputResolver(input);

        Listen();
    }

    /// <summary>
    /// Найти необходимое действие в соответствии с командой
    /// </summary>
    /// <param name="input"></param>
    protected abstract void InputResolver(string input);

    /// <summary>
    /// Вывести в консоль меню команд
    /// </summary>
    protected void PrintMenu()
    {
        _options.ToList().ForEach(option => Console.WriteLine($"{option.Key}) {option.Value}"));
    }

    /// <summary>
    /// Вывести в консоль сообщение о том, что команда не опозднана, а так же для напоминания вывести меню доступных команд
    /// </summary>
    protected virtual void PrintNoCommandRecognized()
    {
        Console.WriteLine("Неизвестная команда. Введите один из номеров ниже:");
        PrintMenu();
    }

    /// <summary>
    /// Вывести наименование операции по её ключу
    /// </summary>
    /// <param name="key"></param>
    protected void PrintOperationNameByKey(string key)
    {
        Console.WriteLine($"Операция: {_options.GetValueOrDefault(key)}");
    }

    /// <summary>
    /// Вывести сообщение об успехе
    /// </summary>
    protected void PrintSuccess()
    {
        Console.WriteLine("Операция успешно завершена");
    }

    /// <summary>
    /// Вывести сообщение о прошедших с вызова метода миллисекунд
    /// </summary>
    /// <param name="milliseconds"></param>
    protected void PrintHowMuchMillisecondsHavePassed(long milliseconds)
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

    /// <summary>
    /// Получить от пользователя целочисленное число
    /// </summary>
    /// <param name="askingMessage"></param>
    /// <returns></returns>
    protected int AskUserForNumber(string askingMessage)
    {
        Console.WriteLine(askingMessage);
        return int.Parse(Console.ReadLine());
    }

    /// <summary>
    /// Получить от пользователя коллекцию чисел, вводимых по нажатию enter
    /// </summary>
    /// <param name="askingMessage"></param>
    /// <returns></returns>
    protected IEnumerable<int> AskUserForNumbersCollection(string askingMessage)
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
    protected string AskUserForFilePath(string? askingMessage = null)
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
