using System;
using System.Collections.Generic;
using System.Text;
using aoc.core.Exceptions;

namespace aoc.core.solutions
{
    public class SolutionsException : AdventOfCodeException
    {
        public SolutionsException(int code, string title, string message, Exception e) : base(code, title, message, e)
        {
        }

        public SolutionsException(int code, string title) : this(code, title, null, null)
        {
        }
    }
    public class ErrorWhileExecutingScriptException : SolutionsException
    {
        public ErrorWhileExecutingScriptException(string message, Exception e) : base(406, "Error while executing, probably bad input, though it could be a bug in the code!", message, e)
        {
        }
        public ErrorWhileExecutingScriptException(string message) : base(406, "Error while executing, probably bad input, though it could be a bug in the code!", message, null)
        {
        }
        public ErrorWhileExecutingScriptException() : base(406, "Error while executing, probably bad input, though it could be a bug in the code!")
        {
        }
    }
    public class InterpreterNotFoundException : SolutionsException
    {
        public InterpreterNotFoundException(string message) : base(404, "Interpreter not found!", message, null)
        {
        }
        public InterpreterNotFoundException() : base(404, "Interpreter not found!")
        {
        }
    }
                 
    public class UnexpectedResultLengthException : SolutionsException
    {
        public UnexpectedResultLengthException (string message) : base(404, "Unexpected result length!", message, null)
        {
        }
        public UnexpectedResultLengthException () : base(404, "Unexpected result length!")
        {
        }
    }
}
