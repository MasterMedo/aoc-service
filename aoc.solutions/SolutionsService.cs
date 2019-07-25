using System;
using System.Diagnostics;
using System.IO;
using aoc.core.Exceptions;
using aoc.core.helper;
using aoc.core.solutions;
using Microsoft.Extensions.Configuration;

namespace aoc.solutions
{
    public class SolutionsService : ISolutionsService
    {
        private readonly IConfiguration _config;
        private readonly IHelper _helper;

        public SolutionsService(IConfiguration config, IHelper helper)
        {
            _config = config;
            _helper = helper;
        }
        public SolutionsResponse GetSolution(SolutionsRequest request)
        {
            if (!_helper.IsDayYearValid(request.Day, request.Year))
                throw new DayOrYearNotValidException();

            var script = _helper.GetScriptPath(request.Year, request.Day, "python");
            if(!File.Exists(script))
                throw new DayNotImplementedException();

            var python2 = _config["python2"];
            var python3 = _config["python3"];
            if(!File.Exists(python2) || !File.Exists(python3))
                throw new InterpreterNotFoundException();

            File.WriteAllText(@"..\input\" + request.Day + ".txt", request.Input);

            var result = RunScript(python2, script);

            var parts = result.Split("\r\n");
            if (parts.Length != 2)
                throw new UnexpectedResultLengthException(result);

            return new SolutionsResponse
            {
                Part1 = parts[0],
                Part2 = parts[1]
            };
        }

        public string RunScript(string interpreter, string script)
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = interpreter, Arguments = script, RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(processInfo))
                {
                    var err = process?.StandardError.ReadToEnd();
                    if (err != "")
                        throw new ErrorWhileExecutingScriptException();

                    var result = process?.StandardOutput.ReadToEnd();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new ErrorWhileExecutingScriptException(e.Message, e);
            }
        }
    }
}
