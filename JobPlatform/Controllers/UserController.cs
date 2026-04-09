using JobPlatformBackend.API.Contracts.User.Shared;
using JobPlatformBackend.API.Contracts.User.Update;
using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Contracts.Contracts.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace JobPlatformBackend.API.Controllers
{
	[ApiController]
	[Route("api/v1/Users")]

	public class UserController(IUserService userService,ICloudinaryService cloudinaryService):ControllerBase
	{
		
		private readonly IUserService _userService=userService;
		private readonly ICloudinaryService _cloudinaryService=cloudinaryService;

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


		[HttpPost("upload-image")]
		[Authorize] 
		public async Task<IActionResult> UploadProfilePicture(IFormFile file)
		{
			if (file == null || file.Length == 0)
<<<<<<< HEAD
				return BadRequest("Please upload a valid image.");

 			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null) return Unauthorized();

			int userId = int.Parse(userIdClaim.Value);

			try
			{
				var imageUrl = await _userService.UpdateUserProfilePictureAsync(file, userId);
				return Ok(new { Message = "Profile picture updated successfully.", ImageUrl = imageUrl });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
=======
			{
				return BadRequest("No file uploaded.");
			}
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
			{
				return Unauthorized();
			}
			 



				var ImageUrl  = await _userService.UpdateUserProfilePictureAsync( file,userId);
 				return Ok(new { Message = "Profile picture updated successfully.", ImageUrl = ImageUrl });
			
			

>>>>>>> 68131f3b835cca866197072156b25dc63d360e5d
		}

	}
}
