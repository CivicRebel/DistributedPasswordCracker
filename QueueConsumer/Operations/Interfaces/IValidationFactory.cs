using QueueMessageTypes;

namespace QueueConsumer.Operations.Interfaces
{
    internal interface IValidationFactory
    {
        ValidationStep GetValidationChainFromOptions(PasswordCrackingOptions options);
    }
}
