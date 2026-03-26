using JobPlatformBackend.Domain.src.Abstractions;
using JobPlatformBackend.Domain.src.Entity;
using JobPlatformBackend.Infrastructure.src.Database;
using Microsoft.Extensions.Options;
using System.Security.Claims;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace JobPlatformBackend.Business.src.Managers
{
	public class JwtManager
	{
		private readonly JwtOptions _options;
		private readonly IUserRepository _userRepository;
		private readonly AppDbContext _dbcontext;

		public JwtManager(IOptions<JwtOptions>options,IUserRepository userRepository,AppDbContext appDbContext)
		{
			_options = options.Value;
			_userRepository = userRepository;
			_dbcontext = appDbContext;
		}

		public string GenerateAccessToken(User user)
		{
			var claims = new List<Claim> {
			new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
			new Claim (ClaimTypes.Email,user.Email),
			new Claim(ClaimTypes.Role,user.Role.ToString())
			};
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
			var singingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var securityTokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer=_options.Issuer,
				Audience=_options.Audience,
				Expires=DateTime.UtcNow.AddMinutes(15),
				Subject=new ClaimsIdentity(claims),
				SigningCredentials=singingCredentials
			};
			var token = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);
			string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenValue;
		}

		public string GenerateTempToken(string email)
		{
			var claims = new[] { new Claim(ClaimTypes.Email, email) };

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _options.Issuer,
				audience: _options.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(10),
				signingCredentials: creds
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
