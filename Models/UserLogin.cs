using System.ComponentModel.DataAnnotations;
namespace C_Sharp_Login_Registration.Models
{
    public class UserLogin
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email : ")]
        [Required(ErrorMessage="Email Must be Entered")]
        public string LoginEmail {get;set;}

        [DataType(DataType.Password)]
        [Display(Name="Password : ")]
        [Required(ErrorMessage="Password Must be Entered")]
        public string LoginPassword {get;set;}
    }
}