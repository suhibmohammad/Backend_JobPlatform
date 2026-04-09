namespace JobPlatformBackend.Contracts.Contracts.User.Create

{
	public record CreateUserResponse
	(
 	string FName,
 	string LName,
 	string Email,
	string Role,
	bool Active,
	DateTime RegistrationDate)
	{

	}
}
