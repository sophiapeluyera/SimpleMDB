using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;

namespace SimpleMDB
{
    public class Result<T>
    {
        public bool IsValid { get; }
        public T? Value { get; }
        public Exception? Error { get; }

        public Result(T value)
        {
            IsValid = true;
            Value = value;
            Error = null;
        }

        public Result(Exception error)
        {
            IsValid = false;
            Value = default;
            Error = error;
        }
    }
}
