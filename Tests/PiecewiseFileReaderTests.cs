using Implementation;
using Interfaces;

namespace Tests;

public class PiecewiseFileReaderTests
{
    [Fact]
    public void ChunkRead_CorrectOneReadOneLineFile_WhenChunkSizeLessThanStringLength()
    {
        IPiecewiseReader reader = new PiecewiseFileReader("Datasets/SimpleFile.txt", 10);
        var actual = reader.ReadNextChunk();
        var expected = "Hello, Fil";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChunkRead_CorrectOneReadOneLineFile_WhenChunkSizeGreaterThanStringLength()
    {
        IPiecewiseReader reader = new PiecewiseFileReader("Datasets/SimpleFile.txt", 100);
        var actual = reader.ReadNextChunk();
        var expected = "Hello, File Reader!";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChunkRead_CorrectOneReadMultiplyLineFile_WhenChunkSizeGreaterThanStringLength()
    {
        IPiecewiseReader reader = new PiecewiseFileReader("Datasets/MultilineFile.txt", 100);
        var actual = reader.ReadNextChunk();
        var expected = "Hello,\r\nFile Reader!";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChunkRead_CorrectOneReadMultiplyLineFileReadNewLine_WhenChunkSizeLessThanStringLength()
    {
        IPiecewiseReader reader = new PiecewiseFileReader("Datasets/MultilineFile.txt", 7);
        var actual = string.Empty;
        while (!reader.IsEndOfRead())
            actual += reader.ReadNextChunk();

        var expected = "Hello,\r\nFile Reader!";

        Assert.Equal(expected, actual);
    }
}