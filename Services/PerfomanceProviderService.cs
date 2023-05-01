using System.Diagnostics;

namespace AlgsAndDataStructures.Services;

/// <summary>
/// Интерфейс для сервиса отслеживания производительности выполнения методов
/// </summary>
public interface IPerfomanceProviderService
{
    /// <summary>
    /// Выполнить метод, получить, сколько это заняло времени
    /// </summary>
    /// <param name="method"></param>
    /// <returns>Количество милисекунд, которое прошло с запуска метода</returns>
    public long RunToCheckPerfomance(Action method);

    /// <summary>
    /// Выполнить метод, получить, сколько это заняло времени. Можно также получить результат метода
    /// </summary>
    /// <param name="method"></param>
    /// <returns>Количество милисекунд, которое прошло с запуска метода</returns>
    public long RunToCheckPerfomance(Func<object> method, out object? functionResult);
}

internal class PerfomanceProviderService : IPerfomanceProviderService
{
    private readonly Stopwatch _stopwatch = new Stopwatch();

    public long RunToCheckPerfomance(Action method)
    {
        _stopwatch.Start();
        method();
        _stopwatch.Stop();
        return _stopwatch.ElapsedMilliseconds;
    }

    public long RunToCheckPerfomance(Func<object> method, out object? methodResult)
    {
        _stopwatch.Start();
        methodResult = method();
        _stopwatch.Stop();
        return _stopwatch.ElapsedMilliseconds;
    }
}
