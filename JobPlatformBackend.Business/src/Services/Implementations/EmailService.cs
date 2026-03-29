using JobPlatformBackend.Business.src.Services.Abstractions;
using MimeKit;
 using MailKit.Security;
using MailKit.Net.Smtp;



namespace JobPlatformBackend.Business.src.Services.Implementations
{
	public class EmailService : IEmailService
	{
		private readonly string _smtpServer="smtp.gmail.com";
		private readonly int _smtpPort=465;
		private readonly string _fromEmail = "doroob70@gmail.com";
		private readonly string _password = "flpmodkpxikxczax";
		public async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlMessage)
		{
			try
			{
				var email=new MimeMessage();
				email.From.Add(new MailboxAddress("Doroob", _fromEmail));
				email.To.Add(new MailboxAddress("",toEmail));

				email.Subject = subject;
				email.Body=new TextPart("html")
				{
					Text = htmlMessage
				};
				using var smtp=new SmtpClient();
				await smtp.ConnectAsync(_smtpServer,_smtpPort, SecureSocketOptions.SslOnConnect);
				await smtp.AuthenticateAsync(_fromEmail, _password);
				await smtp.SendAsync(email);
				await smtp.DisconnectAsync(true);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
