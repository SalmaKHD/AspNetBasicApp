namespace FirstWebApplication.Core.Exceptions
{
    public class CustomInvalidArgumentException : ArgumentException
    {
        public CustomInvalidArgumentException()
        {
        }

        public CustomInvalidArgumentException(string message) : base(message)
        {
        }

        public CustomInvalidArgumentException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}