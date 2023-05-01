using AlgsAndDataStructures.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgsAndDataStructures.Helpers;

public static class RandomHelpers
{
    public static IEnumerable<int> GetRandomNumbers(int count)
    {
        return Enumerable.Range(0, count).PickRandom(count);
    }
}
