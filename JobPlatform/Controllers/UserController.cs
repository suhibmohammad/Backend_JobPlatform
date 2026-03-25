using JobPlatformBackend.API.Contracts.User.Shared;
using JobPlatformBackend.API.Contracts.User.Update;
using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Contracts.Contracts.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JobPlatformBackend.API.Controllers
{
	[ApiController]
	[Route("/v1/Users")]
	public class UserController(IUserService _userService):ControllerBase
	{

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUser([FromQuery] QueryOptions queryOptions)
		{
			var users = await _userService.GetAllUserAsync(queryOptions);
			return Ok(users);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult>GetUserById(int id)
		{
			var user=await _userService.GetUserByIdAsync(id);
			return Ok(user);
		}

		[HttpGet("Email/{email}")]
		public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
		{
			var user = await _userService.GetUserByEmailAsync(email);
			if (user == null)
			{
				return NotFound("Not found user ");
			}
			return Ok(user);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>>DeleteUserByIdAsync(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);

			if (user == null)
				return NotFound("not found this user");

			return await _userService.DeleteUserByIdAsync(id);

		}
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateUser(int id,UpdateUserRequest request)
		{
			var updateUser = await _userService.UpdateUserAsync(id, request);
			return Ok(updateUser);
		}
	}
}
