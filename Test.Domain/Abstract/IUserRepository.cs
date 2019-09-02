using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;

namespace Test.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
    }
}
