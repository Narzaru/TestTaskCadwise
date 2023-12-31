﻿using Implementation.TextProcessors;

namespace Tests;

// TODO(Narzaru) someday I rewrite tests with theory attribute
public class DelimiterCleanerTests
{
    [Fact]
    public void ProcessChunk_StringWithDelimitersInTheListOfDelimitersToRemove_ReturnCorrectProcessedString()
    {
        var delimitersCleaner = new DelimitersCleaner(new List<char> { ',', '!', '-', '=', ' ' });
        var chunk = "aboba,,,boba, doda, jotm! bkv = lsd";
        var actual = delimitersCleaner.ProcessChunk(chunk);
        var expected = "aboba boba doda jotm bkv lsd";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ProcessChunk_StringWithDelimitersNotInTheListOfDelimitersToRemove_ReturnCorrectProcessedString()
    {
        var delimitersCleaner = new DelimitersCleaner(new List<char> { ',', ' ' });
        var chunk = "aboba,,,boba, doda, jotm!   bkv = fsd";
        var actual = delimitersCleaner.ProcessChunk(chunk);
        var expected = "aboba boba doda jotm! bkv = fsd";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ProcessChunk_EmptyString_ReturnEmptyProcessedStringAndNoThrow()
    {
        var delimitersCleaner = new DelimitersCleaner(new List<char> { ',', ' ' });
        var chunk = "";
        var actual = string.Empty;
        var exception = Record.Exception(() => actual = delimitersCleaner.ProcessChunk(chunk));
        Assert.True(string.IsNullOrEmpty(actual) && exception is null);
    }

    [Fact]
    public void
        ProcessChunk_StringWithPartDelimitersInTheListOfDelimitersToRemoveAndWithNewLines_ReturnCorrectProcessedString()
    {
        var delimitersCleaner = new DelimitersCleaner("!:= ");
        var chunk = "abob,a,\nboba!\nvova = avov";
        var actual = delimitersCleaner.ProcessChunk(chunk);
        var expected = "abob,a,\nboba\nvova avov";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ProcessChunk_StringWithDelimitersAndNewLinesInTheListOfDelimiters_ReturnCorrectProcessedString()
    {
        var delimitersCleaner = new DelimitersCleaner(",!:=\r\n ");
        var chunk = "abob,a,\nboba!\nvova = avov";
        var actual = delimitersCleaner.ProcessChunk(chunk);
        var expected = "abob a boba vova avov";
        Assert.Equal(expected, actual);
    }
}

public class WordRemoverTests
{
    [Theory]
    [InlineData(new[] { "aboba", " bob\r\n", "adoda", " avova", "afofa" }, "aboba \r\nadoda avovaafofa", 4)]
    [InlineData(new[] { "aboba ", "bob\r\n", "adoda ", "avova", "afofa" }, "aboba \r\nadoda avovaafofa", 4)]
    [InlineData(new[] { "aboba\r\n", "bob\r\n", "adoda\r\n", "avova\r\n" }, "aboba\r\n\r\nadoda\r\navova\r\n", 5)]
    public void ProcessChunk_DifferenceLines_ReturnCorrectProcessedString(
        string[] chunks,
        string expected,
        int minimumWordLength)
    {
        var wordRemover = new WordRemover(minimumWordLength);
        var actual = string.Empty;

        foreach (var chunk in chunks) actual += wordRemover.ProcessChunk(chunk);

        actual += wordRemover.Final();

        Assert.Equal(expected, actual);
    }
}