using JobPlatformBackend.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Abstractions
{
	public interface IVerificationService
	{
		string GenerateOtp();
		string HashOtp(string otp);
		bool VerifyOtp(string otp, string hashedOtp);
		 Task<bool> ConfirmResetPasswordAsync(string email, string otp, string newPassword)ك


		void SetVerification(User user, string hashedOtp, TimeSpan expiry);
		Task SendEmailVerificationAsync(User user);
		Task SendPasswordResetCodeAsync(User user);

		Task <bool> VerifyEmailCodeAsync(User user, string otp);


	}
}
