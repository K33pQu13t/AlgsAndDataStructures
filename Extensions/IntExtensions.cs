using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgsAndDataStructures.Extensions;

public static class IntExtensions
{
    /// <summary>
    /// Получить количество цифр в числе
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int CountOfDigits(this int value)
    {
        if (value == 0)
        {
            return 1;
        }
        if (value < 0)
        {
            value *= -1;
        }
        return (int)Math.Log10(value) + 1;
    }
}
