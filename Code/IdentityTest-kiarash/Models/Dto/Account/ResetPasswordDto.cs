using System.ComponentModel.DataAnnotations;

namespace IdentityTest.Models.Dto.Account
{
    public class ResetPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
        public string UserId { get; set; }  
        public string Token { get; set; }
    }
}
