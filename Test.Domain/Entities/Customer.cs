using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Display(Name = "Phone numbers")]
        public int PhoneNumber { get; set; }
    }
}
