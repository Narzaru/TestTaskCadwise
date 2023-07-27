using Interfaces;

namespace Implementation;

public class FileProcessor
{
    private readonly IPiecewiseFileReader[] _readers;

    private readonly IPiecewiseTextProcessor[] _textProcessors;

    private readonly IPiecewiseFileWriterFabric _piecewiseFilePiecewiseWriterFabric;

    public FileProcessor(
        IEnumerable<IPiecewiseFileReader> readers,
        IEnumerable<IPiecewiseTextProcessor> textProcessors,
        IPiecewiseFileWriterFabric writerFabric)
    {
        _readers = readers.ToArray();
        _textProcessors = textProcessors.ToArray();
        _piecewiseFilePiecewiseWriterFabric = writerFabric;
    }

    public async Task Run()
    {
        foreach (var reader in _readers)
            await Task.Run(() =>
            {
                try
                {
                    using var writer = _piecewiseFilePiecewiseWriterFabric.NewFileWriter(reader.FullFilePath());
                    while (!reader.IsEndOfFile())
                    {
                        var chunk = reader.ReadNextChunk();
                        foreach (var textProcessor in _textProcessors) chunk = textProcessor.ProcessChunk(chunk);
                        foreach (var textProcessor in _textProcessors) chunk += textProcessor.Final();

                        writer.WriteChunk(chunk);
                    }
                }
                catch
                {
                    // ignore
                }
            });
    }
}