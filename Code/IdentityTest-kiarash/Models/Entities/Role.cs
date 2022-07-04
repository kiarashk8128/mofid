using Microsoft.AspNetCore.Identity;

namespace IdentityTest.Models.Entities
{
    public class Role:IdentityRole

    {
        public string Description { get; set; }

    }
}
