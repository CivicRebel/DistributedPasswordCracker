namespace QueueConsumer.Operations.Interfaces
{
    public interface IValidationChainBuilder
    {
        ValidationStep ValidationChain { get; }
        IValidationChainBuilder GetValidationChain();
        IValidationChainBuilder AddSecondCallApiTesting();
        IValidationChainBuilder AddPayloadFiltering();
        IValidationChainBuilder AddPayloadSizeFiltering();
        IValidationChainBuilder AddJWTFiltering();
    }
}
