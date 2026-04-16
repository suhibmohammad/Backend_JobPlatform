using System.ComponentModel.DataAnnotations;

namespace JobPlatformBackend.Contracts.Contracts.User.Create
{
	public record CreateUserRequests(
		[Required] string FName,
		[Required] string LName,
		[Required] string Email,
 		[Required, MinLength(4)] string Password,
		  string? PhoneNumber,
 	string? Location
  		)
	{
	}
}
