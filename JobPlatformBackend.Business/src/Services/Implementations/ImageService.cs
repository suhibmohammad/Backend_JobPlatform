using JobPlatformBackend.Business.src.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Implementations
{
	public class ImageService : IImageService
	{

		private readonly ICloudinaryService _cloudinaryService;

		public ImageService(ICloudinaryService cloudinaryService)
		{
			_cloudinaryService = cloudinaryService;
		}


		public async Task DeleteAsync(string publicId)
		{
			if (!string.IsNullOrEmpty(publicId))
			{
				await _cloudinaryService.DeleteImageAsync(publicId);
			}
		}

		public async Task<(string Url, string PublicId)> ReplaceAsync(IFormFile file, string folder, string oldPublicId)
		{
			var result = await _cloudinaryService.UploadImageAsync(file, folder);

 			if (!string.IsNullOrEmpty(oldPublicId))
			{
				await _cloudinaryService.DeleteImageAsync(oldPublicId);
			}

			return (result.SecureUrl.ToString(), result.PublicId);
		}

		public async Task<(string Url, string PublicId)> UploadAsync(IFormFile file, string folder)
		{
			var result=await _cloudinaryService.UploadImageAsync(file, folder);
			return (result.SecureUrl.ToString(), result.PublicId);
		}
	}
}
