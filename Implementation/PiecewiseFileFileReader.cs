using Interfaces;

namespace Implementation;

public sealed class PiecewiseFileFileReader : IPiecewiseFileReader
{
    private readonly string _filePath;
    private readonly StreamReader _reader;
    private int _bufferSize;

    public PiecewiseFileFileReader(string filePath, int maxStringLength = 1024)
    {
        _filePath = filePath;
        _reader = new StreamReader(filePath);
        SetChunkSize(maxStringLength);
    }

    public bool EndOfFile => _reader.EndOfStream;

    public void Dispose()
    {
        _reader.Dispose();
    }

    public void SetChunkSize(int chunkSize)
    {
        _bufferSize = chunkSize;
    }

    public string ReadNextChunk()
    {
        var buffer = new char[_bufferSize];
        var readChars = _reader.Read(buffer, 0, _bufferSize);
        var chunkString = new string(buffer, 0, readChars);

        return chunkString;
    }

    public bool IsEndOfFile()
    {
        return EndOfFile;
    }

    public string FullFilePath()
    {
        return _filePath;
    }
}