using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Core.AdventOfCode
{
    public interface IAdventOfCodeService
    {
        AdventOfCodeResponse GetResult(AdventOfCodeRequest request);
    }
}
