using System.ComponentModel.DataAnnotations;

namespace IdentityTest.Models.Dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
