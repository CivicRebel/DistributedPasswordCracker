using MassTransit;

namespace QueuePublisher.Consumers
{
    internal class PasswordMatchedConsumerDefinition: ConsumerDefinition<PasswordMatchedConsumer>
    {
        public PasswordMatchedConsumerDefinition() {
            EndpointName = "passwordMatchedQueue";
            ConcurrentMessageLimit = 1;
        }
    }
}
