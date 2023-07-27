using System.Text;
using Interfaces;

namespace Implementation;

public class PiecewiseFileWriterFabric : IPiecewiseFileWriterFabric
{
    private readonly string _outputDirectory;
    private readonly string _fileSuffix;

    public PiecewiseFileWriterFabric(string outputDirectory, string fileSuffix = "")
    {
        _outputDirectory = outputDirectory;
        _fileSuffix = fileSuffix;
    }

    public IPiecewiseFileWriter NewFileWriter(string filePath)
    {
        return new PiecewiseFileWriterStrategy(FormFilePath(filePath));
    }

    public IPiecewiseWriter NewWriter(object? o)
    {
        if (o is not string s) throw new InvalidCastException("The object is not of type string.");
        return new PiecewiseFileWriterStrategy(s);
    }

    private string FormFilePath(string filePath)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(_outputDirectory);
        stringBuilder.Append(Path.GetFileNameWithoutExtension(filePath));
        stringBuilder.Append(_fileSuffix);
        stringBuilder.Append(Path.GetExtension(filePath));

        return stringBuilder.ToString();
    }
}