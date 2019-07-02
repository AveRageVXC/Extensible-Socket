using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensibleSocket
{
    public class SendResult : Result
    {
        public long BytesSent { get; set; }

        public SendResult(bool success, string data, string errorText, Exception error, bool hasError, long bytesSent) : base(success, data, errorText, error, hasError)
        {
            Success = success;
            HasError = hasError;
            Data = data;
            Error = error;
            ErrorText = errorText;
            BytesSent = bytesSent;
        }
    }
}
