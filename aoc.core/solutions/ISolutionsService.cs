using System;
using System.Collections.Generic;
using System.Text;

namespace aoc.core.solutions
{
    public interface ISolutionsService
    {
        SolutionsResponse GetSolution(SolutionsRequest request);
    }
}
