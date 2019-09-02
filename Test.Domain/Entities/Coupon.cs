using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Domain.Entities
{
    public class Coupon
    {
        [Key]
        public int CouponID { get; set; }
        public string CouponNumber { get; set; }
        public bool CouponUsed { get; set; }
        public int? CouponOwner { get; set; }
        public int? TransactionID { get; set; }
    }
}
