using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensibleSocket
{
    public class Result
    {
        public bool Success { get; set; }
        public bool HasError { get; set; }
        public string Data { get; set; }
        public string ErrorText { get; set; }
        public Exception Error { get; set; }

        public Result(bool success, string data, string errorText, Exception error, bool hasError)
        {
            Success = success;
            HasError = hasError;
            Data = data;
            Error = error;
            ErrorText = errorText;
        }
    }
}
