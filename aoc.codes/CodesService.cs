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

            var language = request.Language ?? Constants.DefaultLanguage;
            var script = _helper.BuildScriptPath(request.Year, request.Day, language);

            if(!File.Exists(script))
                throw new DayNotImplementedException();

            return new CodesResponse
            {
                Language = language,
                Content = File.ReadAllText(script)
            };
        }
    }
}
