using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Abstract;
using Test.Domain.Entities;

namespace Test.Domain.Concrete
{
    public class EFCouponRepository : ICouponRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Coupon> Coupons
        {
            get { return context.Coupons; }
        }

        public void RedeemCoupon(Transaction transaction)
        {
            if (transaction.TransactionID == 0)
            {
                Coupon dbEntry = context.Coupons.FirstOrDefault(x=> x.CouponNumber == transaction.CouponNumber);
                if (dbEntry.CouponOwner == null)
                {
                    dbEntry.CouponOwner = transaction.CustomerID;
                    dbEntry.CouponUsed = true;
                    dbEntry.TransactionID = transaction.TransactionID;
                }
            }
            if (transaction.TransactionID != 0)
            {
                Transaction temp = context.Transactions.Find(transaction.TransactionID);
                Coupon couponOld = context.Coupons.FirstOrDefault(x => x.CouponNumber == temp.CouponNumber);
                Coupon couponNew = context.Coupons.FirstOrDefault(x => x.CouponNumber == transaction.CouponNumber);
                if (temp.CouponNumber != null)
                {
                    couponOld.CouponOwner = null;
                    couponOld.CouponUsed = false;
                    couponOld.TransactionID = null;
                    couponNew.CouponOwner = transaction.CustomerID;
                    couponNew.CouponUsed = true;
                    couponNew.TransactionID = transaction.TransactionID;
                }
            }     
            context.SaveChanges();
        }
    }
}
