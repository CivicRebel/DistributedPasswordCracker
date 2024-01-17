using Newtonsoft.Json;
using QueueMessageTypes;
using System.Text;

namespace QueuePublisher.FileHandlers
{   
    public class ResultsWriter
    {
        private readonly StreamWriter _writer;

        public ResultsWriter(string filePath)
        {
            var stream = new FileStream(filePath, FileMode.Append);
            _writer = new StreamWriter(stream, Encoding.UTF8);
        }

        public void WriteResult(PasswordMatchedEvent evt)
        {
            var serializedResult = JsonConvert.SerializeObject(evt);
            _writer.WriteLine(serializedResult);
            _writer.FlushAsync();
        }
    }
}
