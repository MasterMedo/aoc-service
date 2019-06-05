using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using aoc.core.solutions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace aoc.solutions
{
    public class SolutionsService : ISolutionsService
    {
        public SolutionsResponse GetSolution(SolutionsRequest request)
        {
            if (request.Day < 1 || request.Day > 25 || request.Year < 2015 || request.Year > 2018)
                throw new DayOrYearNotValidException();

            var json = File.ReadAllText("aoc-config.json");
            var config = JsonConvert.DeserializeObject<Config>(json);

            var script = config.Script + "\\" + request.Year + "\\day\\" + request.Day + ".py";
            File.WriteAllText(config.Input + "\\" + request.Day + ".txt", request.Input);

            if(!File.Exists(script))
                throw new DayNotImplementedException();
            if(!File.Exists(config.Python2))
                throw new InterpreterNotFoundException();

            var (result, err) = RunScript(config.Python2, script);

            if (err != "")
                throw new ErrorWhileExecutingScriptException();

            var parts = result.Split("\r\n");
            var response = new SolutionsResponse();

            if (parts.Length < 2)
                return response;

            response.Part1 = parts[0];
            response.Part2 = parts[1];
            return response;
        }

        private static (string, string) RunScript(string python, string script)
        {
            var processInfo = new ProcessStartInfo
                {FileName = python, Arguments = script, RedirectStandardOutput = true, RedirectStandardError = true};

            using (var process = Process.Start(processInfo))
            {
                var err = process.StandardError.ReadToEnd();
                var result = process.StandardOutput.ReadToEnd();
                return (result, err);
            }
        }
    }
}
