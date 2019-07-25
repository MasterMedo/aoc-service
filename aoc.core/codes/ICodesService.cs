using System;
using System.Collections.Generic;
using System.Text;

namespace aoc.core.codes
{
    public interface ICodesService
    {
        CodesResponse GetCode(CodesRequest request);
    }
}
