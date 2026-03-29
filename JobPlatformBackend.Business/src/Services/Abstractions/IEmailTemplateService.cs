namespace JobPlatformBackend.Business.src.Services.Abstractions
{
	public interface IEmailTemplateService
	{
		string GetWelcomeEmail(string userName);
		string GetVerificationEmail(string otp);
		string GetResetPasswordEmail(string otp);
	}
}
