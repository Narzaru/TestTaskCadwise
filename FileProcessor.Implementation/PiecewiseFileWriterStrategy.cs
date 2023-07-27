using Interfaces;

namespace Implementation;

public class PiecewiseFileWriterStrategy : IPiecewiseFileWriter
{
    private readonly StreamWriter _fileStream;
    private readonly string _filePath;

    public PiecewiseFileWriterStrategy(string filePath)
    {
        _filePath = filePath;
        _fileStream = new StreamWriter(filePath);
    }

    public void WriteChunk(string chunk)
    {
        _fileStream.Write(chunk);
    }

    public string FullFilePath()
    {
        return _filePath;
    }

    public void Dispose()
    {
        _fileStream.Dispose();
    }
}