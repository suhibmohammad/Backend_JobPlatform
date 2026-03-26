using System.Net;

namespace JobPlatformBackend.Domain.src.Exceptions
{
	public class NotFoundException : AppException
	{
		public NotFoundException(string message) : base(message, (int)HttpStatusCode.NotFound) { }
	}
}
