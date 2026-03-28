 using JobPlatformBackend.API.Contracts.User.Shared;
using JobPlatformBackend.API.Contracts.User.Update;
using JobPlatformBackend.Contracts.Contracts.User.Create;
using JobPlatformBackend.Contracts.Contracts.UserSkill;
using JobPlatformBackend.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Mappers
{
	public static class DtoMapperUser
	{
		public static CreateUserResponse ToDto(this User user)
		{
			return new CreateUserResponse(user.Name, user.Email, user.Role.ToString(), user.Active, DateTime.UtcNow);
		}

		public static UserDto ToUserIncludeDto(this User user)
		{
			return new UserDto(user.Id, user.Name, user.Email, user.Role.ToString(), user.Active, user.PhoneNumber, user.ProfileImageUrl, user.Headline, user.Location, user.About, user.UserSkills.Select(s => new UserSkillDto(s.Skill.Name)).ToList());

		}

		public static Expression<Func<User, UserDto>> ToUserDto =
			user => new UserDto(user.Id, user.Name, user.Email, user.Role.ToString(), user.Active, user.PhoneNumber, user.ProfileImageUrl, user.Headline, user.Location, user.About, user.UserSkills.Select(s => new UserSkillDto(s.Skill.Name)).ToList());
		
 	}
	}

