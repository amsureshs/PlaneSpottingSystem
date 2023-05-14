﻿using System;
namespace Rusada.Common.Exceptions
{
	public abstract class ApiException : Exception
	{
        public ApiException()
            : base()
        {
        }

        public ApiException(string message)
			: base(message)
		{
		}

        public ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

