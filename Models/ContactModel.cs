using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace DutchTreat.Models
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }

        private string _postalCode;
        const int POSTALCODE_SPACE = 3;
        // Date and time when the contact form was created to save it in db
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Data annotation to make it Required and Regular expression to validate that more than 2 and less than 50 characters have been entered.
        [Required]
        [MinLength(2, ErrorMessage = "First Name must have at least 2 characters long and a maximum of 50 characters.")]
        [MaxLength(50)]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "First Name cannot contain numbers.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        // Data annotation to make it Required and Regular expression to validate that more than 2 and less than 50 characters have been entered.
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must have at least 2 characters long and a maximum of 50 characters.")]
        [MaxLength(50)]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Last Name cannot contain numbers.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Data annotation to make it Required and Regular expression to validate Canadian postal code and saving it in db always upper case with space.
        //https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference
        //https://stackoverflow.com/questions/15774555/efficient-regex-for-canadian-postal-code-function
        //https://www.reddit.com/r/dotnet/comments/92bzad/c_how_to_apply_toupper_or_other_function_to_each/?rdt=59260
        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^(?!\s)([A-Za-z]\d[A-Za-z]\s?\d[A-Za-z]\d)$", ErrorMessage = "Error! Must be valid postal code (X1X 1X1)")]
        public string PostalCode 
        { 
            get 
            { 
                return _postalCode; 
            } 
            set
            {
                _postalCode = value.ToUpper().Replace(" ", "");
                _postalCode = _postalCode.Insert(POSTALCODE_SPACE, " ");
            }
                
        }

        // Data annotation to make it Required and Regular expression to validate that a valid email.
        [Required]
        [MinLength(6, ErrorMessage = "Please enter a valid email address.")]
        [MaxLength(254)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@".*@.*\.\w{2,}", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        // Data annotation to make Regular expression to validate that a valid phone number.
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number.")]
        public string Phone { get; set; }

        // Data annotation to make it Required to validate that a topic was chosen.
        [Required]
        public string Topic { get; set; }

        // Data annotation to make it Required and to validate that the max length for comments or questions has been entered.
        [Required]
        [MaxLength(300)]
        [Display(Name = "Question or Comment")]
        public string QorC { get; set; }

        // Class to handle backend reCAPTCHA response
        public class CaptchaResponse
        {
            public bool Success { get; set; }
        }
    }
}

