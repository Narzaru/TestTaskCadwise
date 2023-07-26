using Interfaces;

namespace Implementation;

public class FileProcessor
{
    public FileProcessor(
        IEnumerable<IPiecewiseReader> readers,
        IEnumerable<IPiecewiseTextWrite> writers,
        IEnumerable<IPiecewiseTextProcessor> textProcessors)
    {
        _readers = readers.ToArray();
        _textProcessors = textProcessors.ToArray();
    }

    public async Task Run()
    {
        foreach (var reader in _readers)
        {
            await Task.Run(() =>
            {
                while (!reader.IsEndOfRead())
                {
                    var chunk = reader.ReadNextChunk();
                    foreach (var textProcessor in _textProcessors)
                    {
                        chunk = textProcessor.ProcessChunk(chunk);
                    }

                    Console.WriteLine(chunk);
                }
            });
        }
    }

    private readonly IPiecewiseReader[] _readers;
    private readonly IPiecewiseTextProcessor[] _textProcessors;
}