using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Frontend.Exceptions
{
    public class FileTooBigException : Exception
    {
        public FileTooBigException()
        {
        }

        protected FileTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FileTooBigException(string? message) : base(message)
        {
        }

        public FileTooBigException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}