namespace QueueMessageTypes
{
    public class PasswordsChunk
    {
        public PasswordCrackingOptions Options { get; set; } = new PasswordCrackingOptions();
        public IEnumerable<string> Usernames { get; set; } = [];
        public IEnumerable<string> Passwords { get; set; } = []; 
    }
}
