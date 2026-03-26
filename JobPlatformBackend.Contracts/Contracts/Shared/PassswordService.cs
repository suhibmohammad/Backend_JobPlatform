using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Contracts.Contracts.Shared
{
	public static class PassswordService
	{
		public static string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}
		public static bool VerifyPassword(string hashedPassword,string providedPassword) { 
		return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
		}
	}
}
