using System.ComponentModel.DataAnnotations;

namespace Back_end_API.Handle
{
    public class Validate
    {
        public static bool IsValidEmail(string email)
        {
            var checkEmail = new EmailAddressAttribute();
            return checkEmail.IsValid(email);
        }
    }
}
