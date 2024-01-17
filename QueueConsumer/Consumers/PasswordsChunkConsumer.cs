using MassTransit;
using QueueConsumer.Operations;
using QueueMessageTypes;

namespace QueueConsumer.Consumers
{
    public class PasswordChunkConsumer : IConsumer<PasswordsChunk>
    {
        private readonly PasswordCrackingService _service;
        public PasswordChunkConsumer(PasswordCrackingService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<PasswordsChunk> context)
        {
            await _service.ProcessChunk(context.Message);
        }
    }
}
