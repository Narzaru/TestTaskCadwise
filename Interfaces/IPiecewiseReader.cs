namespace Interfaces;

public interface IPiecewiseReader
{
    public void SetChunkSize(int chunkSize);

    public string ReadNextChunk();

    public bool IsEndOfRead();
}