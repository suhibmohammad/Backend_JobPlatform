using System.Net;

namespace JobPlatformBackend.Domain.src.Exceptions
{
	public class DuplicateSkillException : AppException
	{
		public DuplicateSkillException(string message) : base(message,(int)HttpStatusCode.Conflict) { }
	}
}
