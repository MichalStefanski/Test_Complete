using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test.Domain.Entities;
namespace Test.WebUI.Models
{
    public class CouponViewModel
    {
        [Key]
        public int CouponID { get; set; }
        public string CouponNumber { get; set; }
        public bool CouponUsed { get; set; }
        public int? CouponOwner { get; set; }
        public int? TransactionID { get; set; }
    }
}