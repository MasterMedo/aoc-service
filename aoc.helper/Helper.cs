using System;
using aoc.core;
using aoc.core.helper;
using Microsoft.Extensions.Configuration;

namespace aoc.helper
{
    public class Helper : IHelper
    {
        private readonly IConfiguration _config;

        public Helper(IConfiguration config)
        {
            _config = config;
        }
        public bool IsDayYearValid(int day, int year)
        {
            if (day < Constants.StartDay || day > Constants.EndDay || year < Constants.StartYear || year > Constants.EndYear)
                return false;

            var today = DateTime.UtcNow;
            var target = new DateTime(year, Constants.Month, day, Constants.UtcReleaseHour, 0, 0);
            return (today - target).TotalSeconds > 0;
        }

        public string BuildScriptPath(int year, int day, string language)
        {
            return _config["AdventOfCodeRoot"] + '\\' + year + @"\day\" + day + GetLanguageExtension(language);
        }

        public string GetLanguageExtension(string language)
        {
            switch (language)
            {
                case null:
                    throw new ArgumentNullException("Programming");
                case "python2":
                    return ".py";
                case "python3":
                    return ".py";
                default:
                    throw new ProgrammingLanguageNotRecognizedException();
            }
        }
    }
}
