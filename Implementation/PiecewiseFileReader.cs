using Interfaces;

namespace Implementation;

public class PiecewiseFileReader : IPiecewiseReader, IDisposable
{
    protected readonly StreamReader Reader;
    protected int BufferSize;

    public PiecewiseFileReader(string filePath, int maxStringLength = 1024)
    {
        Reader = new StreamReader(filePath);
        SetChunkSize(maxStringLength);
        _disposed = false;
    }

    public bool EndOfFile => Reader.EndOfStream;

    public void SetChunkSize(int chunkSize)
    {
        BufferSize = chunkSize;
    }

    public string ReadNextChunk()
    {
        var buffer = new char[BufferSize];
        var readChars = Reader.Read(buffer, 0, BufferSize);
        var chunkString = new string(buffer, 0, readChars);

        return chunkString;
    }

    public bool IsEndOfRead()
    {
        return EndOfFile;
    }

    #region DisposeRegion

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                // Dispose managed resources (StreamReader)
                Reader.Dispose();

            // Dispose unmanaged resources (none in this case)

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}