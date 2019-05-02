namespace CustomerAppPaymentP.Abstractions
{
    public interface IPaymentProcessorPlugin
    {
        string GetName();

        IPaymentProcessor ReadPaymentProcessor();
    }
}