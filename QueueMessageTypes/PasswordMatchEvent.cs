namespace QueueMessageTypes
{
    public class PasswordMatchedEvent
    {
        public string ApiEndpoint { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
