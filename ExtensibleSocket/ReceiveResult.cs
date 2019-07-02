using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensibleSocket
{
    public class ReceiveResult : Result
    {
        public long BytesReceived { get; set; }

        public ReceiveResult(bool success, string data, string errorText, Exception error, bool hasError, long bytesReceived) : base(success, data, errorText, error, hasError)
        {
            Success = success;
            HasError = hasError;
            Data = data;
            Error = error;
            ErrorText = errorText;
            BytesReceived = bytesReceived;
        }
    }
}
