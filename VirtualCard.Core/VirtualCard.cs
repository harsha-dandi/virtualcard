using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualCard.Core
{
    public class VirtualCard : IVirtualCard
    {
        private readonly ReaderWriterLockSlim _lock;
        private decimal _balance;

        public VirtualCard()
        {
            _lock = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// I am assuming overdraft is limitless and not checking .
        /// </summary>
        /// <param name="amount">amount as input</param>
        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Input cannot be negetive");
            }

            _lock.EnterWriteLock();
            _balance -= amount;
            _lock.ExitWriteLock();
        }

        public void TopUp(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Input cannot be negetive");
            }

            _lock.EnterWriteLock();
            _balance += amount;
            _lock.ExitWriteLock();
        }

        public decimal GetBalance()
        {
            _lock.EnterReadLock();
            var localbal = _balance;
            _lock.ExitReadLock();

            return localbal;
        }
    }
}
