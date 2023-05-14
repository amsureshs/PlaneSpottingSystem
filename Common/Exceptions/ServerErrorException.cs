using System;
namespace Rusada.Common.Exceptions
{
	public class ServerErrorException : ApiException
	{
        public ServerErrorException()
            : base()
        {
        }

        public ServerErrorException(string message)
            : base(message)
        {
        }

        public ServerErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

