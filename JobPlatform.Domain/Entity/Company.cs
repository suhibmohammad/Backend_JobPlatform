namespace JobPlatform.Domain.Entity
{
	public class Company : SharedEnitity
	{
		public string Name { get; set; }
 		public string Descriptoin { get; set; }
		public string Email { get; set; }
		public ICollection<User> Admins { get; set; }=new List<User>();

		public ICollection<Job> Jobs { get; set; }= new List<Job>();
	}

}
