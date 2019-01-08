using System;

namespace VirtualCard.Core
{
    public class PinValidator : IValidatePin
    {
        private readonly int _minimumCharacters;
        private readonly int _maximumCharecters;

        private const string ValidPin = "3345";

        public PinValidator(int minimumCharacters, int maximumCharecters)
        {
            if (minimumCharacters == 0 || maximumCharecters == 0)
            {
                throw new ArgumentException("Minimum or maximum charecters cannot be Zero");
            }

            if (maximumCharecters < minimumCharacters)
            {
                throw new ArgumentException("Minimum charecters must be greater than MaximumCharecters");
            }

            _maximumCharecters = maximumCharecters;
            _minimumCharacters = minimumCharacters;
        }

        public bool Validate(string pin)
        {
            if (string.IsNullOrEmpty(pin))
            {
                throw new ArgumentNullException(nameof(pin));
            }

            if (pin.Length < _minimumCharacters || pin.Length > _maximumCharecters)
            {
                throw new ArgumentException("Invaild Input");
            }

            return pin == ValidPin;
        }

    }
}