namespace IdentityTest.Models.Dto.Account
{
    public class TwoFactorLoginDto
    {
        public string Code { get; set; }
        public bool RememberMe { get; set; }
        public string Provider { get; set; }  
    }
}
