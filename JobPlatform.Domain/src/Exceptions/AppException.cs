// File: Domain/Exceptions/AppException.cs
public class AppException : Exception
{
	public int StatusCode { get; }
	public object? AdditionalData { get; }

	public AppException(string message, int statusCode, object? additionalData = null)
		: base(message)
	{
		StatusCode = statusCode;
		AdditionalData = additionalData;
	}
}