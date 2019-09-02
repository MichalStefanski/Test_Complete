using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Abstract;
using Test.Domain.Entities;

namespace Test.Domain.Concrete
{
    public class EFCouponClaimPrizeRepository : ICouponClaimPrizeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<CouponClaimPrize> CouponClaimPrizes
        {
            get { return context.CouponClaimPrizes; }
        }
    }
}
