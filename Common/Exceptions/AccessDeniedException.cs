using System;
using System.Net;

namespace Common.Exceptions
{
    public class AccessDeniedException : AppException
    {
        public AccessDeniedException()
            : base(ApiResultStatusCode.AccessDenied, HttpStatusCode.NotFound)
        {
        }

        public AccessDeniedException(string message)
            : base(ApiResultStatusCode.AccessDenied, message, HttpStatusCode.NotFound)
        {
        }

        public AccessDeniedException(object additionalData)
            : base(ApiResultStatusCode.AccessDenied, null, HttpStatusCode.NotFound, additionalData)
        {
        }

        public AccessDeniedException(string message, object additionalData)
            : base(ApiResultStatusCode.AccessDenied, message, HttpStatusCode.NotFound, additionalData)
        {
        }

        public AccessDeniedException(string message, Exception exception)
            : base(ApiResultStatusCode.AccessDenied, message, exception, HttpStatusCode.NotFound)
        {
        }

        public AccessDeniedException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.AccessDenied, message, HttpStatusCode.NotFound, exception, additionalData)
        {
        }
    }
}