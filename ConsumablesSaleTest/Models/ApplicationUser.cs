using Microsoft.AspNetCore.Identity;

namespace ConsumablesSaleTest.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
