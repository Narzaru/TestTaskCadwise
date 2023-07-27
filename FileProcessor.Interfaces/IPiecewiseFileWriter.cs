namespace Interfaces;

public interface IPiecewiseFileWriter : IPiecewiseWriter, IDisposable
{
    public string FullFilePath();
}