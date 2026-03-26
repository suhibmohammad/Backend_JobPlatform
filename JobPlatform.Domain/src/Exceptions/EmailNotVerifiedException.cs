using System.Net;

namespace JobPlatformBackend.Domain.src.Exceptions
{
	public class EmailNotVerifiedException

		: AppException
	{
		public EmailNotVerifiedException(string message) : base(message, (int)HttpStatusCode.Conflict, new { requiresVerification = true }) { }
	}
}
