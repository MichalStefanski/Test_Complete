using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Abstract;
using Test.Domain.Entities;

namespace Test.Domain.Concrete
{
    public class EFTransactionRepository :ITransactionRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Transaction> Transactions
        {
            get { return context.Transactions; }
        }

        public void AddTransaction(Transaction transaction)
        {
            Transaction dbEntry = context.Transactions.Find(transaction.TransactionID);
            if (dbEntry == null)
            {
                context.Transactions.Add(transaction);
            }
            else
            {
                if (dbEntry != null)
                {
                    dbEntry.TransactionID = transaction.TransactionID;
                    dbEntry.CustomerID = transaction.CustomerID;
                    dbEntry.PrizeID = transaction.PrizeID;
                    dbEntry.CouponNumber = transaction.CouponNumber;
                    dbEntry.TransactionNumber = transaction.TransactionNumber;
                    dbEntry.TransactionValue = transaction.TransactionValue;
                    dbEntry.TransactionDate = transaction.TransactionDate;
                    dbEntry.PrizeRecieved = transaction.PrizeRecieved;
                }
            }
            context.SaveChanges();
        }

        public void ConfirmPrize(int id)
        {
            Transaction dbEntry = context.Transactions.Find(id);
            dbEntry.PrizeRecieved = true;
            context.SaveChanges();
        }
    }
}
