using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.WebUI.Models
{
    public class FormViewModel
    {
        public CustomerViewModel GetCustomerViewModel { get; set; }
        public ContactViewModel GetContactViewModel { get; set; }
        public TransactionViewModel GetTransactionViewModel { get; set; }
        public CouponViewModel GetCouponViewModel { get; set; }
        public PrizeViewModel GetPrizeViewModel { get; set; }

        public List<TransactionViewModel> TransactionViewModels { get; set; }
        public List<CustomerViewModel> CustomerViewModels { get; set; }
        public List<CouponViewModel> CouponViewModels { get; set; }
        public IEnumerable<SelectListItem> PrizeViewModels { get; set; }
    }
}