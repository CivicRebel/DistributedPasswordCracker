namespace QueueMessageTypes
{
    public class PasswordCrackingOptions
    {
        public int ProcessableBatchSize { get; set; }
        public string VictimApiEndpoint { get; set; } = string.Empty;
        public bool EnableSecondApiCallTesting { get; set; } = false;
        public string SecontApiCallTestingEndpoint { get; set; } = string.Empty;
        public bool EnablePayloadFiltering { get; set; } = false;
        public IEnumerable<string> WordlistToFilter { get; set; } = [];
        public bool EnablePayloadSizeFiltering { get; set; } = false;
        public int PayloadSizeInBytes { get; set; }
        public bool ApplyJWTFilter { get; set; } = false;
    }
}
