﻿using System;

namespace WebAPI.Exceptions
{
    public class RequestException : Exception
    {
        public RequestException(string message) : base(message) { }
    }
}
