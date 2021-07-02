

using ApiSkeletonPoc.Core.Insfrastucture.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Insfrastucture.ErrorHandler
{
    public class ErrorHandler : IErrorHandler
    {


        public string GetMessage(ErrorMessagesEnum message)
        {
            switch (message)
            {
                case ErrorMessagesEnum.EntityNull:
                    return "The entity passed is null {0}. Additional information: {1}";
                case ErrorMessagesEnum.ModelValidation:
                    return "The request data is not correct. Additional information: {0}";
                case ErrorMessagesEnum.ContactNotFound:
                    return "The contact you are looking for doesn't exists.";
                default:
                    throw new ArgumentOutOfRangeException(nameof(message), message, null);
            }

        }
    }
}
