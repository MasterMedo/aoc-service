using System;
using System.Collections.Generic;
using System.Text;

namespace aoc.core.Exceptions
{
    public class AdventOfCodeException : Exception
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public AdventOfCodeException(int code, string title, string message, Exception e) : base(message, e)
        {
            Title = title;
            Code = code;
        }

        public AdventOfCodeException(int code, string title) : this(code, title, null, null)
        {
        }
    }
    public class DayNotImplementedException : AdventOfCodeException
    {
        public DayNotImplementedException(string message) : base(501, "Day is not implemented yet :/", message, null)
        {
        }
        public DayNotImplementedException() : base(501, "Day is not implemented yet :/")
        {
        }
    }
    public class DayOrYearNotValidException : AdventOfCodeException
    {
        public DayOrYearNotValidException(string message) : base(406, "Day or year not valid!", message, null)
        {
        }
        public DayOrYearNotValidException() : base(406, "Day or year not valid!")
        {
        }
    }
}
