namespace Interfaces;

public interface IPiecewiseFileReader : IPiecewiseReader, IDisposable
{
    public string FullFilePath();
}