using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Abstract;
using Test.Domain.Entities;

namespace Test.Domain.Concrete
{
    public class EFPrizeRepository : IPrizeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Prize> Prizes
        {
            get { return context.Prizes; }
        }
    }
}
