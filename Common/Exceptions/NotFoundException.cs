using System;
using System.Net;

namespace Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(ApiResultStatusCode.NotFound, message, HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(object additionalData)
            : base(ApiResultStatusCode.NotFound, null, HttpStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(ApiResultStatusCode.NotFound, message, HttpStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(ApiResultStatusCode.NotFound, message, exception, HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.NotFound, message, HttpStatusCode.NotFound, exception, additionalData)
        {
        }
    }
}
