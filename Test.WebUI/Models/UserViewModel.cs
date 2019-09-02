using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.WebUI.Models
{
    public class UserViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Login")]        
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}