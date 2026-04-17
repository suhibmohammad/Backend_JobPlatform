using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Contracts.Contracts.Application
{
	public record ApplicationResponse(
		int Id,
		int UserId,
		string UserName,
		string UserEmail,
		int JobId,
		string JobTitle,
		DateTime AppliedAt,
		string CvUrl
	);
	
	
}
