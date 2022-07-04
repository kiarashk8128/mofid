using System.ComponentModel;

namespace ConsoleApp2.View;

public enum RegisterMenuResponses : byte
{
   // [Description("Register Successful")]
    REGISTER_SUCCESSFUL,
    //[Description("invalid password")]
    INVALID_PASSWORD,
 //   [Description("invalid username")]
    INVALID_USERNAME,
   // [Description("password is weak")]
    WEAK_PASSWORD,
  //  [Description("user with this username already exists")]
    USER_WITH_THIS_USERNAME_EXISTS,
    LOGIN_SUCCESSFUL,
    NO_USER_WITH_THIS_USERNAME_EXISTS,
    PASSWORD_IS_WRONG,
    PASSWORD_CHANGED_SUCCESFULLY,
    INVALID_OLD_PASSWORD,
    INVALID_NEW_PASSWORD,
    NEW_PASSWORD_IS_WEAK,
    OLD_PASSWORD_IS_WEAK,
    REMOVED_SUCCESSFULLY
    
    
}