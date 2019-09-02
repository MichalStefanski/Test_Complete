using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Domain.Entities
{
    public class Prize
    {
        [Key]
        public int PrizeID { get; set; }
        public string PrizeName { get; set; }
    }
}
