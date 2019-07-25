using System;
using System.Collections.Generic;
using System.Text;
using aoc.core.Exceptions;

namespace aoc.core.helper
{
    public class HelperException : AdventOfCodeException
    {
        public HelperException(int code, string title, string message, Exception e) : base(code, title, message, e)
        {
        }
        public HelperException(int code, string title) : base(code, title)
        {
        }
    }

    public class ProgrammingLanguageNotRecognizedException : HelperException
    {
        private static readonly int _code = 400;
        private static readonly string _title = "Programming language not recognized.";
        public ProgrammingLanguageNotRecognizedException(string message, Exception e) : base(_code, _title, message, e)
        {
        }
        public ProgrammingLanguageNotRecognizedException(string message) : base(_code, _title, message, null)
        {
        }
        public ProgrammingLanguageNotRecognizedException() : base(_code, _title)
        {
        }
    }
}
