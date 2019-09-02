using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.WebUI.Models
{
    public class ContactViewModel
    {
        [Key]
        public int ContactID { get; set; }

        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Post Code (numbers only)")]
        [Display(Name = "Postal Code")]
        [RegularExpression("^([0-9]{5})$", ErrorMessage = "Post Code should have only numbers")]
        public string PostCode { get; set; }

        [Display(Name = "Email (optional)")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}