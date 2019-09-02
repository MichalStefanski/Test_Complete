using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;

namespace Test.Domain.Abstract
{
    public interface ICouponRepository
    {
        IEnumerable<Coupon> Coupons { get; }

        void RedeemCoupon(Transaction transaction);
    }
}
