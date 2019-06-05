using System;
using System.Collections.Generic;
using System.Text;

namespace aoc.core.solutions
{
    public class SolutionsException : Exception
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public SolutionsException(int code, string title, string message) : base(message)
        {
            Title = title;
            Code = code;
        }

        public SolutionsException(int code, string title) : this(code, title, null)
        {
        }
    }
    public class DayNotImplementedException : SolutionsException
    {
        public DayNotImplementedException(string message) : base(501, "Day is not implemented yet :/", message)
        {
        }
        public DayNotImplementedException() : base(501, "Day is not implemented yet :/")
        {
        }
    }
    public class DayOrYearNotValidException : SolutionsException
    {
        public DayOrYearNotValidException(string message) : base(406, "Day or year not valid!", message)
        {
        }
        public DayOrYearNotValidException() : base(406, "Day or year not valid!")
        {
        }
    }
    public class ErrorWhileExecutingScriptException : SolutionsException
    {
        public ErrorWhileExecutingScriptException(string message) : base(406,
            "Error while executing, probably bad input, though it could be a bug in the code!", message)
        {
        }
        public ErrorWhileExecutingScriptException() : base(406, "Error while executing, probably bad input, though it could be a bug in the code!")
        {
        }
    }
    public class InterpreterNotFoundException : SolutionsException
    {
        public InterpreterNotFoundException(string message) : base(404, "Interpreter not found!", message)
        {
        }
        public InterpreterNotFoundException() : base(404, "Interpreter not found!")
        {
        }
    }
}
