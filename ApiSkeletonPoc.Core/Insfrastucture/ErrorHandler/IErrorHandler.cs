
namespace ApiSkeletonPoc.Core.Insfrastucture.ErrorHandler
{
    public interface IErrorHandler
    {
        string GetMessage(ErrorMessagesEnum message);
    }

    public enum ErrorMessagesEnum
    {
        EntityNull = 1,
        ModelValidation = 2,
        ContactNotFound = 3
    }
}
