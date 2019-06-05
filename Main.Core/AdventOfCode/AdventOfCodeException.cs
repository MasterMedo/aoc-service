using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Core.AdventOfCode
{
    public class AdventOfCodeException : Exception
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public AdventOfCodeException(int code, string title, string message) : base(message)
        {
            Title = title;
            Code = code;
        }

        public AdventOfCodeException(int code, string title) : this(code, title, null)
        {
        }
    }
    public class DayNotImplementedException : AdventOfCodeException
    {
        public DayNotImplementedException(string message) : base(501, "Day is not implemented yet :/", message)
        {
        }
        public DayNotImplementedException() : base(501, "Day is not implemented yet :/")
        {
        }
    }
    public class DayOrYearNotValidException : AdventOfCodeException
    {
        public DayOrYearNotValidException(string message) : base(406, "Day or year not valid!", message)
        {
        }
        public DayOrYearNotValidException() : base(406, "Day or year not valid!")
        {
        }
    }
    public class ErrorWhileExecutingScriptException : AdventOfCodeException
    {
        public ErrorWhileExecutingScriptException(string message) : base(406,
            "Error while executing, probably bad input, though it could be a bug in the code!", message)
        {
        }
        public ErrorWhileExecutingScriptException() : base(406, "Error while executing, probably bad input, though it could be a bug in the code!")
        {
        }
    }
    public class InterpreterNotFoundException : AdventOfCodeException
    {
        public InterpreterNotFoundException(string message) : base(404, "Interpreter not found!", message)
        {
        }
        public InterpreterNotFoundException() : base(404, "Interpreter not found!")
        {
        }
    }
}
