using JobPlatformBackend.API.Contracts.User.Shared;
using JobPlatformBackend.API.Contracts.User.Update;
using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Contracts.Contracts.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPlatformBackend.API.Controllers
{
	[ApiController]
	[Route("api/v1/Users")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUser([FromQuery] QueryOptions queryOptions)
		{
			var users = await _userService.GetAllUserAsync(queryOptions);
			return Ok(users);
		}

		[HttpGet("profile")]
		[Authorize]
		public async Task<ActionResult> GetUserProfileAsync()
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

			if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
			{
				return Unauthorized();
			}

			var user = await _userService.GetUserByIdAsync(userId);
			if (user == null)
				return NotFound();

			return Ok(user);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null) return NotFound();
			return Ok(user);
		}

		[HttpGet("Email/{email}")]
		public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
		{
			var user = await _userService.GetUserByEmailAsync(email);
			if (user == null)
			{
				return NotFound("Not found user");
			}
			return Ok(user);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteUserByIdAsync(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
				return NotFound("not found this user");

			var result = await _userService.DeleteUserByIdAsync(id);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateUser(int id, UpdateUserRequest request)
		{
			var updateUser = await _userService.UpdateUserAsync(id, request);
			return Ok(updateUser);
		}

		[HttpPost("upload-image")]
		[Authorize]
		public async Task<IActionResult> UploadProfilePicture(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest("Please upload a valid image.");

			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
			{
				return Unauthorized();
			}

			try
			{
				var imageUrl = await _userService.UpdateUserProfilePictureAsync(file, userId);
				return Ok(new { Message = "Profile picture updated successfully.", ImageUrl = imageUrl });
			}
			catch (Exception ex)
			{
 				return BadRequest(new { Message = ex.Message });
			}
		}
	}
}