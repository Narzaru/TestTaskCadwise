using System.Text;
using Interfaces;

namespace Implementation.TextProcessors;

public class DelimitersCleaner : IPiecewiseTextProcessor
{
    public DelimitersCleaner(IEnumerable<char> delimiters)
    {
        _delimiters = delimiters.ToArray();
    }

    public string ProcessChunk(string chunk)
    {
        var resultString = new StringBuilder();
        var delimiterFound = false;

        foreach (char character in chunk)
        {
            if (!_delimiters.Contains(character))
            {
                if (delimiterFound)
                {
                    delimiterFound = false;
                    if (character is not ' ' and not '\n' and not '\r')
                    {
                        resultString.Append(' ');
                    }
                }

                resultString.Append(character);
            }
            else
            {
                delimiterFound = true;
            }
        }

        return resultString.ToString();
    }

    public string Final()
        => string.Empty;

    public string ProcessLine(string line)
        => ProcessChunk(line);

    private char[] _delimiters;
}