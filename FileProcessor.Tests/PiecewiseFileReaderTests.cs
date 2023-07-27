using Implementation;
using Interfaces;

namespace Tests;

public class PiecewiseFileReaderTests
{
    [Fact]
    public void ChunkRead_CorrectOneReadOneLineFile_WhenChunkSizeLessThanStringLength()
    {
        IPiecewiseFileReader fileReader = new PiecewiseFileFileReader("Datasets/SimpleFile.txt", 10);
        var actual = fileReader.ReadNextChunk();
        var expected = "Hello, Fil";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChunkRead_CorrectOneReadOneLineFile_WhenChunkSizeGreaterThanStringLength()
    {
        IPiecewiseFileReader fileReader = new PiecewiseFileFileReader("Datasets/SimpleFile.txt", 100);
        var actual = fileReader.ReadNextChunk();
        var expected = "Hello, File Reader!";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChunkRead_CorrectOneReadMultiplyLineFile_WhenChunkSizeGreaterThanStringLength()
    {
        IPiecewiseFileReader fileReader = new PiecewiseFileFileReader("Datasets/MultilineFile.txt", 100);
        var actual = fileReader.ReadNextChunk();
        var expected = "Hello,\r\nFile Reader!";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChunkRead_CorrectOneReadMultiplyLineFileReadNewLine_WhenChunkSizeLessThanStringLength()
    {
        IPiecewiseFileReader fileReader = new PiecewiseFileFileReader("Datasets/MultilineFile.txt", 7);
        var actual = string.Empty;
        while (!fileReader.IsEndOfFile())
            actual += fileReader.ReadNextChunk();

        var expected = "Hello,\r\nFile Reader!";

        Assert.Equal(expected, actual);
    }
}