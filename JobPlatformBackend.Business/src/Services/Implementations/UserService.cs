using JobPlatformBackend.API.Contracts.User.GetAll;
using JobPlatformBackend.API.Contracts.User.Shared;
using JobPlatformBackend.API.Contracts.User.Update;
using JobPlatformBackend.Business.src.Mappers;
using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Contracts.Contracts.Shared;
using JobPlatformBackend.Contracts.Contracts.UserSkill;
using JobPlatformBackend.Domain.src.Abstractions;
using JobPlatformBackend.Domain.src.Entity;
using JobPlatformBackend.Domain.src.Exceptions;
using JobPlatformBackend.Infrastructure.src.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly ICloudinaryService _cloudinaryService;
		private readonly AppDbContext _context;
		private readonly IUserRepository _userRepository;
		private readonly BaseService<User, UserDto> _base;
		private readonly ILogger<UserService> _logger;

		public UserService(
			ICloudinaryService cloudinaryService,
			AppDbContext context,
			IUserRepository userRepository,
			BaseService<User, UserDto> baseService,
			ILogger<UserService> logger)
		{
			_cloudinaryService = cloudinaryService;
			_context = context;
			_userRepository = userRepository;
			_base = baseService;
			_logger = logger;
		}

		public async Task<bool> AddSkillToUserAsync(int userId, string skillName)
		{
			var user = await _context.Users.Include(u => u.UserSkills).FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null) return false;

			var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Name.ToLower() == skillName.ToLower());
			if (skill == null)
			{
				skill = new Skill { Name = skillName };
				_context.Skills.Add(skill);
				await _context.SaveChangesAsync();
			}

			user.UserSkills.Add(new UserSkill { UserID = userId, SkillId = skill.Id });
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteUserByIdAsync(int userId)
		{
			try
			{
				var user = await _userRepository.GetByIdAsync(userId);
				if (user == null) return false;

				var completed = await _userRepository.DeleteUser(user.Id);
				await _context.SaveChangesAsync();
				return completed;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting user");
				throw;
			}
		}

		public async Task<IEnumerable<UserDto>> GetAllUserAsync(QueryOptions queryOptions)
		{
			return await _base.GetAll(queryOptions, DtoMapperUser.ToUserDto);
		}

		public async Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
		{
			var userEntity = await _userRepository.Query()
				.Include(u => u.UserSkills).ThenInclude(us => us.Skill)
				.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);

			return userEntity?.ToUserIncludeDto();
		}

		public async Task<UserDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await _userRepository.Query()
				.Where(x => x.Id == id && x.IsDeleted == false)
				.Select(DtoMapperUser.ToUserDto)
				.FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest updateUserRequest)
		{
			var user = await _userRepository.GetByIdAsync(id);
			if (user == null) return false;

			var requestProps = typeof(UpdateUserRequest).GetProperties();
			foreach (var prop in requestProps)
			{
				var value = prop.GetValue(updateUserRequest);
				if (value != null)
				{
					var userProp = typeof(User).GetProperty(prop.Name);
					if (userProp != null && userProp.CanWrite) userProp.SetValue(user, value);
				}
			}
			await _userRepository.UpdateAsync(user);
			await _userRepository.SaveChangesAsync();
			return true;
		}

		public async Task<string> UpdateUserProfilePictureAsync(IFormFile file, int userId)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			if (user == null) throw new BadRequestException("User not found");

			string? oldPublicId = user.ProfileImagePublicId;
			var uploadResult = await _cloudinaryService.UploadImageAsync(file, "Users/UserProfilePhoto");

			if (uploadResult?.Error != null) throw new Exception(uploadResult.Error.Message);

			user.ProfileImageUrl = uploadResult.SecureUrl.ToString();
			user.ProfileImagePublicId = uploadResult.PublicId;

			await _userRepository.UpdateAsync(user);
			await _userRepository.SaveChangesAsync();

			if (!string.IsNullOrEmpty(oldPublicId)) await _cloudinaryService.DeleteImageAsync(oldPublicId);

			return user.ProfileImageUrl;
		}
	}
}