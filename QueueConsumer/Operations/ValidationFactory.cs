using QueueConsumer.Operations.Interfaces;
using QueueMessageTypes;

namespace QueueConsumer.Operations
{
    public class ValidationFactory: IValidationFactory
    {
        private readonly IValidationChainBuilder _builder;

        public ValidationFactory(IValidationChainBuilder builder) {
            _builder = builder;
        }
        
        ValidationStep IValidationFactory.GetValidationChainFromOptions(PasswordCrackingOptions options)
        {
            //_builder.CreateValidationChain()
            //    .AddX();
            return new ValidationStep();
        }
    }
}
