using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.WebUI.Models
{
    public class PrizeViewModel
    {
        [Key]
        public int PrizeID { get; set; }

        [Display(Name = "Reward")]
        public string PrizeName { get; set; }
    }
}