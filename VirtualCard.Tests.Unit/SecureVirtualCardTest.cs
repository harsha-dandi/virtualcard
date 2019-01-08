using System;
using Moq;
using NUnit.Framework;
using VirtualCard.Core;

namespace VirtualCard.Tests.Unit
{
    [TestFixture]
    public class SecureVirtualCardTest
    {
        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<IValidatePin>();
            _virtualcard = new Mock<IVirtualCard>();

            _validator.Setup(x => x.Validate(_invalidPin)).Returns(false);
            _validator.Setup(x => x.Validate(_validPin)).Returns(true);

            _cardInvalidPin = new SecureVirtualCard(_validator.Object, _virtualcard.Object, _invalidPin);
            _cardValidPin = new SecureVirtualCard(_validator.Object, _virtualcard.Object, _validPin);
        }

        private Mock<IValidatePin> _validator;
        private Mock<IVirtualCard> _virtualcard;
        private readonly string _validPin = "3345";
        private readonly string _invalidPin = "0";
        private SecureVirtualCard _cardInvalidPin;
        private SecureVirtualCard _cardValidPin;


        [Test]
        public void Ctor_Null_Validator_Expect_Exception()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new SecureVirtualCard(null, _virtualcard.Object, _validPin));

            Assert.IsTrue(exception.Message.Contains("pinValidator"));
        }

        [Test]
        public void Ctor_Null_VirtualCard_Expect_Exception()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new SecureVirtualCard(_validator.Object, null, _validPin));

            Assert.IsTrue(exception.Message.Contains("virtualCard"));
        }

        [Test]
        public void GetBalance_ValidPin_Expect_VirtualCardCall()
        {
            _virtualcard.Setup(x => x.GetBalance()).Returns(decimal.MinusOne);

            var balance = _cardValidPin.GetBalance();

            Assert.AreEqual(decimal.MinusOne, balance);
            _validator.Verify(x => x.Validate(_validPin), Times.Once);
        }

        [Test]
        public void GetBalance_InvalidPin_Expect_Exception()
        {
            Assert.Throws<ApplicationException>(
                () => _cardInvalidPin.GetBalance());
        }

        [Test]
        public void Topup_Expect_VirtualCardCall()
        {
            _virtualcard.Setup(x => x.TopUp(decimal.One));
            _cardInvalidPin.TopUp(decimal.One);

            _virtualcard.Verify(x => x.TopUp(decimal.One), Times.Once);
        }

        [Test]
        public void Withdraw_InvalidPin_Expect_Exception()
        {
            Assert.Throws<ApplicationException>(
                () => _cardInvalidPin.Withdraw(decimal.One));
        }

        [Test]
        public void Withdraw_ValidPin_Expect_VirtualCardCall()
        {
            _virtualcard.Setup(x => x.Withdraw(decimal.One));

            _cardValidPin.Withdraw(decimal.One);

            _validator.Verify(x => x.Validate(_validPin), Times.Once);
            _virtualcard.Verify(x => x.Withdraw(decimal.One), Times.Once);
        }
    }
}