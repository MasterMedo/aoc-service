using System;
using System.Collections.Generic;
using System.Text;

namespace aoc.core.helper
{
    public interface IHelper
    {
        bool IsDayYearValid(int day, int year);
        string GetScriptPath(int year, int day, string language);
    }
}
