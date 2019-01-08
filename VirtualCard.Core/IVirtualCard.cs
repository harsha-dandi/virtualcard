using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCard.Core
{
    public interface IVirtualCard
    {
        void Withdraw(decimal amount);
        void TopUp(decimal amount);
        decimal GetBalance();
    }
}
