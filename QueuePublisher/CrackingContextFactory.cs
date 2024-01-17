using MassTransit;
using QueueMessageTypes;
using QueuePublisher.FileHandlers;
using QueuePublisher.Publishers;

namespace QueuePublisher
{
    public sealed class CrackingContextFactory
    {
        private readonly IConfiguration _configuration;
        public CrackingContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DictionaryReader GetDictionaryReader()
        {
            var dictionaryRelativePath = _configuration.GetRequiredSection("CrackingSetup:DictionaryRelativePath").Value;
            var batchSize = Convert.ToInt32(_configuration.GetRequiredSection("CrackingSetup:BatchSize").Value);
            var dictionaryPath = Path.GetFullPath(dictionaryRelativePath);
            return new DictionaryReader(dictionaryPath, batchSize);
        }

        public ResultsWriter GetResultsWriter()
        {
            var relativePath = _configuration.GetRequiredSection("CrackingSetup:ResultsFileRelativePath").Value;
            var absolutePath = Path.GetFullPath(relativePath);
            return new ResultsWriter(absolutePath);
        }

        public QueuePublisherService GetQueuePublisherService(IServiceProvider provider)
        {
            var usernames = ExtractUsernamesFromFile();
            var options = _configuration.GetRequiredSection("CrackingSetup:Options").Get<PasswordCrackingOptions>();
            return new QueuePublisherService(usernames, provider.GetRequiredService<IBus>(), provider.GetRequiredService<DictionaryReader>(), options);
        }

        private IEnumerable<string> ExtractUsernamesFromFile()
        {
            var relativePath = _configuration.GetRequiredSection("CrackingSetup:UsernamesListRelativePath").Value;
            var absolutePath = Path.GetFullPath(relativePath);

            List<string> usernames = [];
            
            using (var usernameStream = new StreamReader(new FileStream(absolutePath, FileMode.Open)))
            {
                string? username;
                while ((username = usernameStream.ReadLine()) != null)
                {
                    usernames.Add(username);
                }
            }

            return usernames;
        }
    }
}
