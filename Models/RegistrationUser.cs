using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace C_Sharp_Login_Registration.Models
{
    public class RegistrationUser
    {
        [Key]
        public int RegUserId {get;set;}
        [Required(ErrorMessage="First Name is required")]
        [Display(Name="First Name : ")]
        [MinLength(3)]
        public string FirstName{get;set;}
        [Required(ErrorMessage="Last Name is required")]
        [Display(Name="Last Name : ")]
        [MinLength(3)]
        public string LastName {get;set;}
        [Required(ErrorMessage="Email is required")]
        [Display(Name="Email : ")]
        [MinLength(8)]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;}
        [Required(ErrorMessage="Password is required")]
        [Display(Name="Password : ")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [Display(Name="Confirm Password : ")]
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;



    }
}