using JobPlatformBackend.Domain.src.Entity;

namespace JobPlatformBackend.Domain.src.Abstractions
{
	public interface IUserRepository:IBaseRepository<User> {
		Task<User> GetUserByEmailAsync(string email);
		Task<User> CreateAdminAsync(User user);
		Task<User> UpdatePassword(string email, string PasswordHash);
		Task<bool> DeleteUser(int id);

	}
}
