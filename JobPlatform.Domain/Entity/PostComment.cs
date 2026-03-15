namespace JobPlatform.Domain.Entity
{
	public class PostComment : SharedEnitity
	{
		public required string Content { get; set; }

		// العلاقة مع Post
		public int PostId { get; set; }
		public Post Post { get; set; } = null!;

		// العلاقة مع User
		public int UserId { get; set; }
		public User User { get; set; } = null!;
	}

}
