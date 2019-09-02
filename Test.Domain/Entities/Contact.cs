using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Domain.Entities
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostCode { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Proszę podać prawidłowy adres e-mail.")]
        public string Email { get; set; }
    }
}
