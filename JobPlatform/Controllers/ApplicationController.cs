using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Contracts.Contracts.Application.Create;
using JobPlatformBackend.Contracts.Contracts.Application.Get;
using JobPlatformBackend.Domain.src.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPlatformBackend.API.Controllers
{
	[ApiController]
	[Route("api/v1/application")]
	public class ApplicationController : ControllerBase
	{
		private readonly IApplicationService _applicationService;
		public ApplicationController(IApplicationService applicationService)
		{

			_applicationService = applicationService;
		}


		[HttpPost("job/applications")]
		[Authorize]
		public async Task<IActionResult> GetApplicationsByJobId([FromBody] GetAllApplicationRequest request)
		{
			var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(nameIdentifier) || !int.TryParse(nameIdentifier, out int userId))
			{
				throw new UnauthorizedAccessException("Invalid or missing user token.");
			}

			var applications = await _applicationService.GetApplicationsByJobIdAsync(userId, request);
			return Ok(applications);
		}
		[HttpPost("apply")]
		[Authorize]
		public async Task<IActionResult> ApplyToJob(int jobId, IFormFile cvFile)
		{
			var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(nameIdentifier) || !int.TryParse(nameIdentifier, out int userId))
			{
				throw new UnauthorizedAccessException("Invalid or missing user token.");
			}

			var result = await _applicationService.ApplyToJobAsync(userId, jobId, cvFile);
			if (!result) return BadRequest();
			return Ok(new { Message = "Application submitted successfully." });
		}
		[HttpDelete("{applicationId}")]
		[Authorize]
		public async Task<IActionResult> DeleteJob(int applicationId)
		{
			var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(nameIdentifier) || !int.TryParse(nameIdentifier, out int userId))
			{
				throw new UnauthorizedAccessException("Invalid or missing user token.");
			}
			var result = await _applicationService.DeleteApplicationAsync(userId, applicationId);
			if (!result) return BadRequest();
			return Ok(new { Message = "Application deleted successfully." });
		}

		[HttpPost("MyApplication")]
		[Authorize]
		public async Task<IActionResult> GetApplicationById(int pageNumber, int pageSize)
		{
			var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(nameIdentifier) || !int.TryParse(nameIdentifier, out int userId))
			{
				throw new UnauthorizedAccessException("Invalid or missing user token.");
			}
			var application = await _applicationService.GetApplicationByIdAsync(userId, pageNumber, pageSize);
			if (application == null) return NotFound();
			return Ok(application);
		}
	 


		[HttpGet("Details")]
		[Authorize]

		public async Task<IActionResult> GetApplicationWithDetailsAsync( int applicationId)
		{
			var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(nameIdentifier) || !int.TryParse(nameIdentifier, out int userId))
			{
				throw new UnauthorizedAccessException("Invalid or missing user token.");
			}
			var application = await _applicationService.GetApplicationDetailsAsync(userId, applicationId);
			if (application == null) return NotFound();
			return Ok(application);
		}

		[HttpPost("Status")]
		[Authorize]
		public async Task <IActionResult> UpdateStatusAsync(int userId,int applicationId,string status)
		{
			var update = await _applicationService.UpdateStatusAsync(userId, applicationId, status);
			if (!update)
				throw new BadRequestException("The status is not update");

			return Ok(new {Message ="Succesfully update"});
		}
	}
}