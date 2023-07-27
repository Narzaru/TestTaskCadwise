namespace Interfaces;

public interface IPiecewiseWriterFabric
{
    IPiecewiseWriter NewWriter(object? o);
}