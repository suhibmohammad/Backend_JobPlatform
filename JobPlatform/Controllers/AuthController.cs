using JobPlatformBackend.Business.src.Managers;
using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Business.src.Services.Implementations;
using JobPlatformBackend.Contracts.Contracts.User.Create;
using JobPlatformBackend.Contracts.Contracts.User.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPlatformBackend.API.Controllers
{

	[ApiController]
	[Route("api/v1/Auth")]
	public class AuthController(IAuthService _authService,JwtManager _jwtManager):ControllerBase
	{



		[HttpPost("login")]

		public async Task<IActionResult> Login(UserCredentials request)
		{
			var result = await _authService.AuthenticateUserAsync(request);
		 
			return Ok(new { Token = result });
		}
		

		[HttpPost("Regester")]
		public async Task<IActionResult> CreateAccount(CreateUserRequests requests)
		{


			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await _authService.CreateUserAsync(requests);
			return Ok(result);
		}
	}
}
