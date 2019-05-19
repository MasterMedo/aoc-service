using System;
using Main.Core.AdventOfCode;

namespace AdventOfCode
{
    public class AdventOfCodeService : IAdventOfCodeService
    {
        public AdventOfCodeResponse GetResult(AdventOfCodeRequest request)
        {
            return new AdventOfCodeResponse {Result = request.Input + request.Day + request.Year};
        }
    }
}
