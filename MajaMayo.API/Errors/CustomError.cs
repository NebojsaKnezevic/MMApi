using MajaMayo.API.Constants;

namespace MajaMayo.API.Errors
{
    public static class CustomError
    {
        public static void Throw<TException>(string customMessage) where TException : Exception, new()
        {
            TException exception = new TException();

            exception.Data[GlobalConstants.CustomErrorMessage] = customMessage;

            throw exception;
        }
    }
}
