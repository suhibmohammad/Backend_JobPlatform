namespace JobPlatform.Domain.Entity
{
	public class Post : SharedEnitity
	{
		public required string Title { get; set; }
		public required string Body { get; set; }

		// العلاقة مع User
		public int UserId { get; set; }
		public User User { get; set; } = null!;

		// Likes
		public ICollection<PostLike> Likes { get; set; } = new List<PostLike>();

		// Comments
		public ICollection<PostComment> Comments { get; set; } = new List<PostComment>();
	}

}
