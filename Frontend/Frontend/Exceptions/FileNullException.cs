using System;
using System.Runtime.Serialization;

namespace Frontend.Exceptions
{
    public class FileNullException : Exception
    {
        public FileNullException()
        {
        }


        protected FileNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FileNullException(string? message) : base(message)
        {
        }

        public FileNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}