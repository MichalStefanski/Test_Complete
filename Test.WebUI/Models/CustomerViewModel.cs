using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Domain.Entities;

namespace Test.WebUI.Models
{
    public class CustomerViewModel
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Enter Phone Number")]
        [Display(Name = "Phone Number")]        
        [Remote("DuplicateCustomer", "Home", AdditionalFields = "CustomerID", HttpMethod = "Post", ErrorMessage = "This number is already in use")]
        [RegularExpression("^([1-9])([0-9]{8})$", ErrorMessage = "Phone Number must be 9 Digits Long and Cannot Strats with 0.")]
        public int PhoneNumber { get; set; }
    }
}