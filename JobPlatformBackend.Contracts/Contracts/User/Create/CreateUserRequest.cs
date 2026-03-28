using System.ComponentModel.DataAnnotations;

namespace JobPlatformBackend.Contracts.Contracts.User.Create
{
	public record CreateUserRequests(
		[Required] string Name,
		[Required] string Email,
 		[Required, MinLength(4)] string Password,
		  string? PhoneNumber,
	string? ProfileImageUrl,
	string? Headline,
	string? Location,
	string? About,
	string? CoverImageUrl,
	int? CompanyId,
	List<int>? SkillIds
		)
	{
	}
}
