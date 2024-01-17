using MassTransit;

namespace QueueConsumer.Consumers
{
    internal class PasswordChunkConsumerDefinition : ConsumerDefinition<PasswordChunkConsumer>
    {
        public PasswordChunkConsumerDefinition()
        {
            EndpointName = "passwordsQueue";
        }
    }
}
