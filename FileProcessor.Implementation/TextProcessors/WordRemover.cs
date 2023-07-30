using System.Text;
using Interfaces;

namespace Implementation.TextProcessors;

public class WordRemover : IPiecewiseTextProcessor
{
    private int _minWordLength;
    private string _trailWord;

    public WordRemover(int limitWordLength)
    {
        if (limitWordLength < 0) throw new ArgumentException("The minimum word length must be positive");

        _minWordLength = limitWordLength;
        _trailWord = string.Empty;
    }

    public string ProcessChunk(string chunk)
    {
        var word = new StringBuilder();
        word.Append(_trailWord);
        var result = new StringBuilder();

        foreach (var character in chunk)
            if (character is not (' ' or '\r' or '\n'))
            {
                word.Append(character);
            }
            else
            {
                if (word.Length >= _minWordLength) result.Append(word);

                result.Append(character);
                word.Clear();
            }

        if (word.Length >= _minWordLength)
        {
            result.Append(word);
            word.Clear();
        }

        _trailWord = word.ToString();
        return result.ToString();
    }

    public string Final()
    {
        return _trailWord.Remove(0);
    }

    public string ProcessLine(string line)
    {
        var processedString = new StringBuilder();
        processedString.Append(ProcessChunk(line));
        processedString.Append(Final());

        return processedString.ToString();
    }
}