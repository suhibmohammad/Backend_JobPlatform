using JobPlatformBackend.Contracts.Contracts.User.Create;
using JobPlatformBackend.Contracts.Contracts.User.GetAll;
using JobPlatformBackend.Domain.src.Entity;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 68131f3b835cca866197072156b25dc63d360e5d

namespace JobPlatformBackend.Business.src.Services.Abstractions
{
	public interface IAuthService
	{
		Task SendVerificationCodeAsync(string email); 
 
		Task<string> AuthenticateUserAsync(UserCredentials userCredentials);

		Task<string> RefreshTokenAsync(string refreshToken);
		Task<User> VerifyEmailAsync(string email, string code);

		Task<bool> ForgotPasswordAsync(string email);

		Task<bool> VerifyResetCodeAsync(string email, string code);

		Task<User> ResetPasswordAsync(string email, string code, string newPassword);
		Task<CreateUserResponse> CreateUserAsync(CreateUserRequests requests);
	}
}
