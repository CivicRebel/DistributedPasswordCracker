namespace QueuePublisher.FileHandlers
{
    public class DictionaryReader
    {
        private readonly StreamReader _stream;
        private readonly int _batchSize;

        public DictionaryReader(string filePath, int batchSize)
        {
            _stream = new StreamReader(new FileStream(@$"{filePath}", FileMode.Open));
            _batchSize = batchSize;
        }

        public List<string> GetNextBatch()
        {
            int counter = 0;
            List<string> batch = new List<string>();

            while (counter < _batchSize)
            {
                var line = _stream.ReadLine();
                if (line == null)
                {
                    _stream.Close();
                    break;
                }

                batch.Add(line);
                counter++;
            }

            return batch;
        }
    }
}
