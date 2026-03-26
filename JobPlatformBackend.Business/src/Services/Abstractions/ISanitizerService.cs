using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Abstractions
{
	public interface ISanitizerService
	{
		string SanitizeHtml(string input);

		T SanitizeDto<T>(T inputDto);
	}
}
