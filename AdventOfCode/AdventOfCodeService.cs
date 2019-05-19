using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Main.Core.AdventOfCode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode
{
    public class AdventOfCodeService : IAdventOfCodeService
    {
        public AdventOfCodeResponse GetResult(AdventOfCodeRequest request)
        {
            var json = Encoding.Default.GetString(Properties.Resources.config).Substring(1);
            var c = JsonConvert.DeserializeObject<Config>(json);

            var script = c.Script + "\\" + request.Year + "\\day\\" + request.Day + ".py";
            File.WriteAllText(c.Input + "\\" + request.Day + ".txt", request.Input);

            var (result, err) = RunScript(c.Python2, script);
            if (err != "")
                (result, err) = RunScript(c.Python, script);

            if (err != "")
                return null;

            var parts = result.Split("\r\n");
            var response = new AdventOfCodeResponse();

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
