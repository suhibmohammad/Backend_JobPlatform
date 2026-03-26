using JobPlatformBackend.API.Contracts.User.Shared;
using JobPlatformBackend.Business.src.Managers;
using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Contracts.Contracts.Shared;
using JobPlatformBackend.Contracts.Contracts.User.Create;
using JobPlatformBackend.Contracts.Contracts.User.GetAll;
using JobPlatformBackend.Contracts.Contracts.User.Shared;
using JobPlatformBackend.Contracts.Contracts.UserSkill;
using JobPlatformBackend.Domain.src.Abstractions;
using JobPlatformBackend.Domain.src.Entity;
using JobPlatformBackend.Domain.src.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Implementations
{
	public class AuthService : IAuthService
	{

		private readonly IUserRepository _userRepository;

		private readonly JwtManager _jwtManager;

		private readonly ISanitizerService _sanitizerService;

		public AuthService(IUserRepository userRepository,JwtManager jwtManager,ISanitizerService sanitizerService)
		{
			_userRepository = userRepository;
			_jwtManager = jwtManager;
			_sanitizerService = sanitizerService;

		}

		public async Task<string> AuthenticateUserAsync(UserCredentials userCredentials)
		{
			var user =await _userRepository.GetUserByEmailAsync(userCredentials.Email)??throw new BadRequestException("Invalid login credentials.");
			if (!user.IsEmailVerified)
			{
				throw new BadRequestException("The email is not verified");
			}
			if (user.IsDeleted)
			{
				throw new NotFoundException("The account is not found");
			}
			var isAuthenticated = PassswordService.VerifyPassword(user.HashPassword, userCredentials.Password);
			if (!isAuthenticated)
			{
				throw new BadRequestException("Invalid login credentials");
			}

			string token = _jwtManager.GenerateAccessToken(user);
			return token;

		}

		public async Task<UserDto> CreateUserAsync(CreateUserRequests requests)
		{
			try { 
			var sanitizedDto=_sanitizerService.SanitizeDto(requests);
				var IsValidEmail = Validator.IsValidEmail(sanitizedDto.Email);
				if (!IsValidEmail)
				{
					throw new ArgumentException("Invalid Email address.");
				}

				var existingUser = await _userRepository.GetUserByEmailAsync(sanitizedDto.Email);
				if (existingUser is not null)
				{
					if (!existingUser.IsEmailVerified) {
						throw new EmailNotVerifiedException("Verification code resent. Please verify your email.");
					}
					throw new ConflictException("A user with this email alredy exist.");
				}

				var userDtoProperties = typeof(CreateUserRequests).GetProperties();
				foreach (var property in userDtoProperties) { 
				var value=property.GetValue(sanitizedDto);
					if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
					{
						throw new ArgumentException($"Property {property.Name} cannot be empty or whitespace.");
					}
				}
				return new UserDto(
	Id: 1,
	Name: "Suhaib",
	Email: "suhaib@example.com",
	Role: "Admin",
	Active: true,
	PhoneNumber: "0791234567",
	ProfileImageUrl: "https://example.com/image.jpg",
	Headline: "Full Stack Developer",
	Location: "Amman",
	About: "Passionate developer يحب يتعلم أشياء جديدة 🔥",
	Skills: new List<UserSkillDto?>
	{
		new UserSkillDto("C#"),
		new UserSkillDto("React"),
		new UserSkillDto("SQL")
	}
);
			} 
			catch (Exception ex) {

				throw;
			}
		}

		public Task<bool> ForgotPasswordAsync(string email)
		{
			throw new NotImplementedException();
		}

		public Task<string> RefreshTokenAsync(string refreshToken)
		{
			throw new NotImplementedException();
		}

		public Task<User> ResetPasswordAsync(string email, string code, string newPassword)
		{
			throw new NotImplementedException();
		}

		public Task SendVerificationCodeAsync(string email)
		{
			throw new NotImplementedException();
		}

		public Task<User> VerifyEmailAsync(string email, string code)
		{
			throw new NotImplementedException();
		}

		public Task<bool> VerifyResetCodeAsync(string email, string code)
		{
			throw new NotImplementedException();
		}
	}
}
