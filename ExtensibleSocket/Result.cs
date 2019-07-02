using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensibleSocket
{
    public class Result
    {
        /// <summary>
        /// Status of function
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Shows if exception was caught
        /// </summary>
        public bool HasError { get; set; }
        /// <summary>
        /// Data that was got (if it's receive function, then data would contain the string that was decoded from bytes and bytes would contain received bytes, and if it's send function, then data would contain status of function)
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// Error's text (if error was caught)
        /// </summary>
        public string ErrorText { get; set; }
        /// <summary>
        /// Error (if error was caught)
        /// </summary>
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
