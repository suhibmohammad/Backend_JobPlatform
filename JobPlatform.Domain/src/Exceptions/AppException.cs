using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Domain.src.Exceptions
{
	public abstract partial class AppException : Exception
	{
		public object? ExtraData { get; }

		public int StatusCode { get; }

		protected AppException(string message, int statusCode, object? extraData = null) : base(message)
		{
			StatusCode = statusCode;
			ExtraData = extraData;
		}
	}
}
