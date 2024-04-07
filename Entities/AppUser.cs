using Microsoft.AspNetCore.Identity;

namespace Login.Entities
{
	public class AppUser: IdentityUser
	{
		public string Firstname { get; set; }
		public string Lastname { get; set; }
	}
}
