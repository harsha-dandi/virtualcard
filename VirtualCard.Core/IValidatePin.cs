namespace VirtualCard.Core
{
    public interface IValidatePin
    {
        bool Validate(string pin);
    }
}