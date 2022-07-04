using System.ComponentModel.DataAnnotations;

namespace IdentityTest.Models.Dto.Account
{
    public class VerifyPhoneNumberDto
    {
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public string Code { get; set; }
    }
}
