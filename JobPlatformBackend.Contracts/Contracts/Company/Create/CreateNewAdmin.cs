using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Contracts.Contracts.Company.Create
{
	public record CreateNewAdmin(int UserId,
		int CompanyId
		);
	
	
}
