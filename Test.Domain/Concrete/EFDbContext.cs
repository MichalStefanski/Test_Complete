using Test.Domain.Entities;
using System.Data.Entity;

namespace Test.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<CouponClaimPrize> CouponClaimPrizes { get; set; }
    } 
}
