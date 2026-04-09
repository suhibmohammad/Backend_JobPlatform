using JobPlatformBackend.Contracts.Contracts.Company.Create;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Abstractions
{
	public interface ICompanyService
	{
		Task CreateCompanyAsync(CreateCompanyRequest request,int userId);
		 Task UpdateCompanyAsync(CreateCompanyRequest request);
		 Task DeleteCompanyAsync(int companyId);
		 Task AddAdminToCompanyAsync(int companyId, int userId);
		 Task RemoveAdminFromCompanyAsync(int companyId, int userId);
		Task<string> UpdateLogoUrlCompany(IFormFile file, int companyId, int userId);
	}
}
