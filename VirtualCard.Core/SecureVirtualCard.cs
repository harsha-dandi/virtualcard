using System;

namespace VirtualCard.Core
{
    public class SecureVirtualCard : IVirtualCard
    {
        private readonly string _pin;
        private readonly IValidatePin _pinValidator;
        private readonly IVirtualCard _virtualCard;

        /// <summary>
        ///     Secure virtual card decorator to validate Pin before debiting Money.
        /// </summary>
        /// <param name="pinValidator">Validator</param>
        /// <param name="virtualCard">Instance of the Virtual Card</param>
        /// <param name="pin">
        ///     I am using this as construtor arg here. But this could quite easily be a console input or anything
        ///     else
        /// </param>
        public SecureVirtualCard(IValidatePin pinValidator, IVirtualCard virtualCard, string pin)
        {
            if (null == pinValidator)
            {
                throw new ArgumentNullException(nameof(pinValidator));
            }

            if (null == virtualCard)
            {
                throw new ArgumentNullException(nameof(virtualCard));
            }

            _pinValidator = pinValidator;
            _virtualCard = virtualCard;
            _pin = pin;
        }

        public void Withdraw(decimal amount)
        {
            if (_pinValidator.Validate(_pin))
            {
                _virtualCard.Withdraw(amount);
                return;
            }

            throw new ApplicationException("Pin Incorrect");
        }

        // Not validating Pin for TopUp
        public void TopUp(decimal amount)
        {
            _virtualCard.TopUp(amount);
        }

        public decimal GetBalance()
        {
            if (_pinValidator.Validate(_pin))
            {
                return _virtualCard.GetBalance();
            }

            throw new ApplicationException("Cannot access account without a valid Pin");
        }
    }
}