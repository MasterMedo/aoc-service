using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using aoc.core;
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

            var language = Constants.DefaultLanguage;
            var script = _helper.BuildScriptPath(request.Year, request.Day, language);
            if(!File.Exists(script))
                throw new DayNotImplementedException();

            var interpreter = _config[language];
            if(!File.Exists(interpreter))
                throw new InterpreterNotFoundException();

            File.WriteAllText(@"..\input\" + request.Day + ".txt", request.Input);

            var (err, result) = RunScript(interpreter, script);

            var parts = result.Split("\r\n");
            if (parts.Length > 2)
                throw new UnexpectedResultLengthException(result);

            return new SolutionsResponse
            {
                Part1 = parts.Length > 0 ? parts[0] : null,
                Part2 = parts.Length > 1 ? parts[1] : null,
                Error = parts.Length > 2 ? "Output: " + string.Join(";", parts.Skip(2)) + (err == null ? "" : "\nError: " + err) : err
            };
        }

        public (string, string) RunScript(string interpreter, string script)
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = interpreter,
                    Arguments = script,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(processInfo))
                {
                    var err = process?.StandardError.ReadToEnd();
                    var result = process?.StandardOutput.ReadToEnd();
                    return (err, result.TrimEnd());
                }
            }
            catch (Exception e)
            {
                throw new ErrorWhileExecutingScriptException(e.Message, e);
            }
        }
    }
}
