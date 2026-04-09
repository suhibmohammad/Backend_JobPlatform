using JobPlatformBackend.Contracts.Contracts.User.Create;
using JobPlatformBackend.Contracts.Contracts.User.GetAll;
using JobPlatformBackend.Domain.src.Entity;

namespace JobPlatformBackend.Business.src.Services.Abstractions
{
	public interface IAuthService
	{
		Task SendVerificationCodeAsync(string email); 
		Task<CreateUserResponse> CreateUserAsync(CreateUserRequests requests);

		Task<string> AuthenticateUserAsync(UserCredentials userCredentials);

		Task<string> RefreshTokenAsync(string refreshToken);
		Task<User> VerifyEmailAsync(string email, string code);

		Task<bool> ForgotPasswordAsync(string email);

		Task<bool> VerifyResetCodeAsync(string email, string code);

		Task<User> ResetPasswordAsync(string email, string code, string newPassword);

	}
}
