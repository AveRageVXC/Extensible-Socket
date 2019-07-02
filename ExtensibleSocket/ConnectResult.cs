using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensibleSocket
{
    public class ConnectResult : Result
    {
        public ConnectResult(bool success, string data, string errorText, Exception error, bool hasError) : base(success, data, errorText, error, hasError)
        {
            Success = success;
            HasError = hasError;
            Data = data;
            Error = error;
            ErrorText = errorText;
        }
    }
}
