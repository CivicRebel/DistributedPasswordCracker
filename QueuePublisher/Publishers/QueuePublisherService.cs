using MassTransit;
using QueueMessageTypes;
using QueuePublisher.FileHandlers;

namespace QueuePublisher.Publishers
{
    public class QueuePublisherService : BackgroundService
    {
        private readonly IBus _bus;
        private readonly IEnumerable<string> _usernames;
        private readonly DictionaryReader _reader;
        private readonly PasswordCrackingOptions _options;

        public QueuePublisherService(IEnumerable<string> usernames, IBus bus,
            DictionaryReader dictionaryReader, PasswordCrackingOptions options)
        {
            _bus = bus;
            _usernames = usernames;
            _reader = dictionaryReader;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                IEnumerable<string> batch;
                while ((batch = _reader.GetNextBatch()).Any())
                {
                    await _bus.Send(MapChunk(batch));
                }

                Dispose();
            }
        }

        private PasswordsChunk MapChunk(IEnumerable<string> passwords)
        {
            return new PasswordsChunk
            {
                Options = _options,
                Passwords = passwords,
                Usernames = _usernames
            };
        }
    }
}
