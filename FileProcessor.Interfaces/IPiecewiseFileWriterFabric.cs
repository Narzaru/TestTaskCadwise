namespace Interfaces;

public interface IPiecewiseFileWriterFabric : IPiecewiseWriterFabric
{
    public IPiecewiseFileWriter NewFileWriter(string outputFilePath);
}