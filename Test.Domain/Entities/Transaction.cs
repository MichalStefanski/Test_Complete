using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public int? CustomerID { get; set; }
        public string CouponNumber { get; set; }
        public decimal TransactionValue { get; set; }
        public string TransactionNumber { get; set; }
        public int? PrizeID {get; set; }
        public DateTime TransactionDate { get; set; }
        public Boolean PrizeRecieved { get; set; }
    }
}
