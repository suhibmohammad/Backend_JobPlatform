using Ganss.Xss;
using JobPlatformBackend.Business.src.Services.Abstractions;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Implementations
{
	public class SanitizerService : ISanitizerService
	{
		private readonly HtmlSanitizer _htmlSanitizer;
		public SanitizerService(HtmlSanitizer htmlSanitizer)
		{
			_htmlSanitizer = htmlSanitizer;
		}

		public T SanitizeDto<T>(T inputDto)
		{
			var properties = typeof(T).GetProperties();
			foreach(var property in properties)
			{
				if(property.PropertyType == typeof(string)&&property.Name.ToLower()!="password"&&property.Name.ToLower()!="imageurl")
				{
                var value=property.GetValue(inputDto);
					if (value != null)
					{
						var sanitizedValue = _htmlSanitizer.Sanitize(value.ToString());
						property.SetValue(inputDto, sanitizedValue);
					}
				}

		}
			return inputDto;
		}

		public string SanitizeHtml(string input)
		{
			return _htmlSanitizer.Sanitize(input);
		}
	}
}
