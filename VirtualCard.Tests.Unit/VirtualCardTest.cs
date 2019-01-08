using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace VirtualCard.Tests.Unit
{
    [TestFixture]
    public class VirtualCardTest
    {
        private Core.VirtualCard _card;

        [SetUp]
        public void SetUp()
        {
            _card = new Core.VirtualCard();
        }

        [Test]
        public void GetBalance_newCard_Expect_Zero()
        {   
            Assert.AreEqual(Decimal.Zero, _card.GetBalance());
        }

        [Test]
        public void Withdraw_negetive_Expect_Exception()
        {
            Assert.Throws<ArgumentException>(() => _card.Withdraw(Decimal.MinusOne));
        }

        [Test]
        public void Withdraw_positive_Expect_CorrectBalance()
        {
            _card.Withdraw(Decimal.One);

            Assert.AreEqual(Decimal.MinusOne, _card.GetBalance());
        }

        [Test]
        public void TopUp_negetive_Expect_Exception()
        {
            Assert.Throws<ArgumentException>(() => _card.TopUp(Decimal.MinusOne));
        }

        [Test]
        public void TopUp_positive_Expect_CorrectBalance()
        {
            _card.TopUp(Decimal.One);

            Assert.AreEqual(Decimal.One, _card.GetBalance());
        }


        [Test]
        public void TopUpAndWithdraw_Expect_CorrectBalance()
        {
            _card.TopUp(Decimal.One);
            _card.Withdraw(Decimal.One);

            Assert.AreEqual(Decimal.Zero, _card.GetBalance());
        }

    }
}
