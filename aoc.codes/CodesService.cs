using System;
using System.IO;
using aoc.core;
using aoc.core.codes;
using aoc.core.Exceptions;
using aoc.core.helper;

namespace aoc.codes
{
    public class CodesService : ICodesService
    {
        private readonly IHelper _helper;

        public CodesService(IHelper helper)
        {
            _helper = helper;
        }
        public CodesResponse GetCode(CodesRequest request)
        {
            if (!_helper.IsDayYearValid(request.Day, request.Year))
                throw new DayOrYearNotValidException();

            var script = _helper.GetScriptPath(request.Year, request.Day, request.Language ?? Constants.DefaultLanguage);

            if(!File.Exists(script))
                throw new DayNotImplementedException();

            return new CodesResponse
            {
                Day = request.Day,
                Year = request.Year,
                Language = request.Language ?? Constants.DefaultLanguage,
                Content = File.ReadAllText(script)
            };
        }
    }
}
