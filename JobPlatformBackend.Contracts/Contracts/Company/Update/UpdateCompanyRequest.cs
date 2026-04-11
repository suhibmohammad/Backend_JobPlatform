using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Contracts.Contracts.Company.Update
{
	public record UpdateCompanyRequest (int companyId,string? Name, String? Description, string? Location);
 
}
