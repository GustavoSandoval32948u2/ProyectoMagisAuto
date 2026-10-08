using System.Security.Cryptography.X509Certificates;

namespace MagisAutoAPI.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }

        public ApiException(string message, int statusCode = StatusCodes.Status400BadRequest)
              : base(message)
        {
            StatusCode = statusCode;
        }

    }
}
