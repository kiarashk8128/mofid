using Microsoft.AspNetCore.Identity;

namespace IdentityTest.Models.Entities
{
    public class User:IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
