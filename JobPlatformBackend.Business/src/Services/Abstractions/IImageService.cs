using JobPlatformBackend.API.Contracts.User.Shared;
using JobPlatformBackend.Contracts.Contracts.User.Create;
using JobPlatformBackend.Contracts.Contracts.User.GetAll;
using JobPlatformBackend.Domain.src.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Abstractions
{
	public interface IImageService
	{
		// لرفع صورة لأول مرة
		Task<(string Url, string PublicId)> UploadAsync(IFormFile file, string folder);

		// لاستبدال صورة قديمة بجديدة (رفع + حذف القديمة)
		Task<(string Url, string PublicId)> ReplaceAsync(IFormFile file, string folder, string oldPublicId);

		// لحذف صورة فقط
		Task DeleteAsync(string publicId);
	}
}
