using System.ComponentModel.DataAnnotations;

namespace IdentityTest.Models.Dto.Account
{
    public class SetPhoneNumberDto
    {
        [Required]
        [RegularExpression(@"(\+98|0)?9\d{9}")]
        public string PhoneNumber { get; set; }
    }
}
