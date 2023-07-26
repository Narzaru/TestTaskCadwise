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

    #region DisposeRegion

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources (StreamReader)
                Reader.Dispose();
            }

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

    public void SetChunkSize(int chunkSize)
        => BufferSize = chunkSize;

    public string ReadNextChunk()
    {
        char[] buffer = new char[BufferSize];
        int readChars = Reader.Read(buffer, 0, BufferSize);
        string chunkString = new string(buffer, 0, readChars);

        return chunkString;
    }

    public bool IsEndOfRead() => EndOfFile;

    public bool EndOfFile => Reader.EndOfStream;
}