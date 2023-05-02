using AlgsAndDataStructures.Repositories;
using AlgsAndDataStructures.Services;
using AlgsAndDataStructures.Services.Puzzle;
using AlgsAndDataStructures.Services.Sorting;
using AlgsAndDataStructures.View;
using Microsoft.Extensions.DependencyInjection;

namespace AlgsAndDataStructures;

internal class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IFileService, FileService>()
            .AddScoped<INumbersStorageService, NumbersStorageService>()
            .AddScoped<IPerfomanceProviderService, PerfomanceProviderService>()
            .AddScoped<ISortingService<int>, EnumerableSortingService<int>>()
            .AddScoped<IFibonacciSolverService, FibonacciSolverService>()
            .AddScoped<INoSenceRecursionSolverService, NoSenceRecursionSolverService>()
            .AddScoped<SortingView>()
            .AddScoped<PuzzlesView>()
            .AddSingleton<INumbersStorageRepository, NumbersStorageRepository>()
            .BuildServiceProvider();

        new MainMenuView(serviceProvider).Run();
    }
}