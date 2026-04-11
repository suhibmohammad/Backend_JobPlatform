using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Contracts.Contracts.Company.Get
{
	public record CompanyResponse(
		int Id,
		string Name,
		string? Description,
		string? Location,
		string? LogoUrl
	);
}
