using MassTransit;
using QueueMessageTypes;
using QueuePublisher.FileHandlers;

namespace QueuePublisher.Consumers
{
    internal class PasswordMatchedConsumer : IConsumer<PasswordMatchedEvent>
    {
        private readonly ResultsWriter _writer;

        public PasswordMatchedConsumer(ResultsWriter writer)
        {
            _writer = writer;
        }

        public Task Consume(ConsumeContext<PasswordMatchedEvent> context)
        {
            _writer.WriteResult(context.Message);
            return Task.CompletedTask;
        }
    }
}
