namespace JobPlatformBackend.API.Contracts.User.GetAll
{
	public record UserListResponse(
		int Id,
		string Name,
 		string Email,
		string Role,
		bool Active
	);

}
