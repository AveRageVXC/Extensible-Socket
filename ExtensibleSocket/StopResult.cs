using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensibleSocket
{
    public class StopResult : Result
    {
        public StopResult(bool success, string data, string errorText, Exception error, bool hasError) : base(success, data, errorText, error, hasError)
        {
            Success = success;
            HasError = hasError;
            Data = data;
            Error = error;
            ErrorText = errorText;
        }
    }
}
