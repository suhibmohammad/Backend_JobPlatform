namespace JobPlatformBackend.Contracts.Contracts.Application.Get
{
	public record ApplicationDetailResponse(
	int ApplicationId,
	int UserId,
	string FullName,
	string Email,
	string CvUrl,
	string Status,
	DateTime AppliedAt,
	List<string> Skills // إذا كان عندك جدول مهارات مربوط بالمستخدم
);

}
