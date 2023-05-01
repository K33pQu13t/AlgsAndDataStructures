using Microsoft.Extensions.DependencyInjection;
using AlgsAndDataStructures.Helpers;

namespace AlgsAndDataStructures.Services;

/// <summary>
/// Интерфейс для сервиса по работе с файлами
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Записать в файл countOfNumbers случайных целых чисел
    /// </summary>
    /// <param name="filePath">путь до файла</param>
    /// <param name="countOfNumbers">количество чисел для генерации</param>
    void WriteRandomNumbers(string filePath, int countOfNumbers, string separator = " ");

    /// <summary>
    /// Записать числа в файл
    /// </summary>
    /// <param name="filePath">путь до файла</param>
    /// <param name="separator">разделитель для чисел</param>
    /// <param name="numbers">числа для записи</param>
    void WriteNumbers(string filePath, string separator = "", params int[] numbers);
    
    /// <summary>
    /// Распечатать содержимое файла
    /// </summary>
    /// <param name="filePath">путь до файла</param>
    void PrintFileContent(string filePath);

    /// <summary>
    /// Распечатать первый элемент файла
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="separator">разделитель элементов файлов</param>
    void PrintFirstElementOfFile(string filePath, string separator = " ");

    /// <summary>
    /// Распечатать последнрий элемент файла
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="separator">разделитель элементов файлов</param>
    void PrintLastElementOfFile(string filePath, string separator = " ");

    /// <summary>
    /// Получить числа из файла
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="separator">разделитель для чисел</param>
    /// <returns></returns>
    IEnumerable<int> GetNumbersFromFile(string filePath, string separator = " ");

    /// <summary>
    /// Поместить числа из файла в глобальное хранилище чисел
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="separator"></param>
    void AddNumbersFromFileToNumbersStorage(string filePath, string separator = " ");
}

public class FileService : IFileService
{
    private readonly INumbersStorageService _numbersStorageService;
    public FileService(IServiceProvider serviceProvider)
    {
        _numbersStorageService = serviceProvider.GetRequiredService<INumbersStorageService>();
    }

    public IEnumerable<int> GetNumbersFromFile(string filePath, string separator = " ")
    {
        ValidateFilePath(filePath);

        string content = File.ReadAllText(filePath);
        IEnumerable<string> words = content.Split(separator);
        return words
            .Where(word => int.TryParse(word, out _))
            .Select(word => int.Parse(word));
    }

    public void PrintFileContent(string filePath)
    {
        ValidateFilePath(filePath);

        string content = File.ReadAllText(filePath);
        Console.WriteLine(content);
    }

    public void PrintFirstElementOfFile(string filePath, string separator = " ")
    {
        ValidateFilePath(filePath);

        string result = string.Empty;
        using(StreamReader reader = new(filePath))
        {
            char nextChar;
            while (reader.Peek() >= 0)
            {
                nextChar = (char)reader.Read();

                result += nextChar;
                if (result.EndsWith(separator))
                {
                    // Выводим результат, не учитывая сепаратор
                    Console.WriteLine(result[..^separator.Length]);
                    return;
                }
            }
        }

        // Если в файле не было разделителя
        Console.WriteLine(result);
    }

    public void PrintLastElementOfFile(string filePath, string separator = " ")
    {
        ValidateFilePath(filePath);

        string result = string.Empty;
        using (FileStream stream = new(filePath, FileMode.Open, FileAccess.Read))
        {
            // Устанавливаем чтение с конца
            stream.Seek(0, SeekOrigin.End);
            byte[] buffer = new byte[1];

            while (stream.Position > 0)
            {
                // Отодвинуть указатель назад на размер буфера
                stream.Seek(-buffer.Length, SeekOrigin.Current);
                // Считать данные в буфер
                stream.Read(buffer, 0, buffer.Length);
                // Отодвинуть указатель снова, чтобы в следующей итерации прочитать следующий блок данных
                stream.Seek(-buffer.Length, SeekOrigin.Current);

                result = $"{(char)buffer[0]}{result}";
                if (result.StartsWith(separator))
                {
                    // Выводим результат, не учитывая сепаратор
                    Console.WriteLine(result[separator.Length..]);
                    return;
                }
            }
        }

        // Если в файле не было разделителя
        Console.WriteLine(result);
    }

    public void WriteNumbers(string filePath, string separator = " ", params int[] numbers)
    {
        ValidateFilePath(filePath);

        string result = string.Join(separator, numbers);

        // Если файл не пустой, начинаем добавление с сепаратора
        if (new FileInfo(filePath).Length > 0)
        {
            result = $"{separator}{result}";
        }
        File.AppendAllText(filePath, result);
    }

    public void WriteRandomNumbers(string filePath, int countOfNumbers, string separator = " ")
    {
        ValidateFilePath(filePath);

        IEnumerable<int> numbers = RandomHelpers.GetRandomNumbers(countOfNumbers);
        WriteNumbers(filePath, separator, numbers.ToArray());
    }

    public void AddNumbersFromFileToNumbersStorage(string filePath, string separator = " ")
    {
        IEnumerable<int> numbers = GetNumbersFromFile(filePath);
        _numbersStorageService.Set(numbers);
    }

    private void ValidateFilePath(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ApplicationException("Путь до файла должен быть указан");
        }
        if (!File.Exists(filePath))
        {
            throw new ApplicationException($"Файл {filePath} не существует");
        }
    }
}
