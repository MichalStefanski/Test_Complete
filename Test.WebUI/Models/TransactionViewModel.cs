using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test.Domain.Entities;
using System.Web.Mvc;

namespace Test.WebUI.Models
{
    public class TransactionViewModel
    {
        [Key]
        public int TransactionID { get; set; }
        public int? CustomerID { get; set; }

        [Display(Name = "Coupon")]
        [Required(ErrorMessage = "Enter coupon code here.")]
        [Remote("FreshCoupon", "Home", HttpMethod = "Post", AdditionalFields = "TransactionID", ErrorMessage = "Incorrect code or already used")]
        public string CouponNumber { get; set; }

        [Display(Name = "Transaction Value")]
        [Required(ErrorMessage = "Enter transaction value")]
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Invalid value")]
        public decimal TransactionValue { get; set; }

        [Display(Name = "Transaction Number")]
        [Required(ErrorMessage = "Enter transaction number")]
        public string TransactionNumber { get; set; }

        public int? PrizeID { get; set; }

        [Display(Name = "Transaction Date")]
        [Required(ErrorMessage = "Enter the transaction date.")]
        [Test.WebUI.Infrastructure.DateRange(ErrorMessage ="Date not in range")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TransactionDate { get; set; }
        public Boolean PrizeRecieved { get; set; }
    }
}