using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.Entities
{
    [Table("CouponClaimPrizeView")]
    public class CouponClaimPrize
    {
        [Key]
        public int CouponID { get; set; }
        public string CouponNumber { get; set; }
        public int? PhoneNumber { get; set; }
        public string PrizeName { get; set; }
    }
}
