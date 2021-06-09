using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using DawProject.Data;
namespace DawProject.Models
{
    public class RegisterE : IValidatableObject
    {
        private LibraryContext context = new LibraryContext();


        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Adresa de E-Mail invalida!")]
        public string Email { get; set; }
        [Required]
        [MembershipPassword(
        MinRequiredNonAlphanumericCharacters = 0,
        MinRequiredPasswordLength = 6
        )]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Parolele sunt diferite!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Data de nastere invalida!")]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(BornDate.Date >= DateTime.Today)
            {
                yield return new ValidationResult(
                    errorMessage: "Data de nastere invalida!",
                    memberNames: new[] { "BornDate" }
               );
            }
            var EMAIL = context.Register.Where(x => (x.Email.Contains(Email) && Email.Contains(x.Email))).Select(x => new
            {
                New_Mail = x.Email
            }).ToList();
            if (EMAIL.Count != 0)
            {
                yield return new ValidationResult(
                    errorMessage: "Adresa de EMail exista deja!",
                    memberNames: new[] { "Email" }
               );
            }
        }
    }
    public class ChangePassword
    {
        [DisplayName("Old Password")]
        [Required]
        [MembershipPassword(
        MinRequiredNonAlphanumericCharacters = 0,
        MinRequiredPasswordLength = 6
        )] 
        public string Password { get; set; }
        [DisplayName("New Password")]
        [Required]
        [MembershipPassword(
        MinRequiredNonAlphanumericCharacters = 0,
        MinRequiredPasswordLength = 6
        )]
        public string ConfirmPassword { get; set; } 
    }
}