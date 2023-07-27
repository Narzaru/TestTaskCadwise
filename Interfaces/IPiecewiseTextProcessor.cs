namespace Interfaces;

public interface IPiecewiseTextProcessor
{
    /// <summary>
    ///     preforms the necessary actions on the string
    /// </summary>
    /// <param name="chunk">
    ///     the input is a part of the string that needs to be processed
    /// </param>
    /// <returns>
    ///     return preprocessed string
    /// </returns>
    string ProcessChunk(string chunk);

    /// <summary>
    ///     Because the file is read in chunks, some file processors may not know when the end of the file is.
    /// </summary>
    /// <returns>
    ///     return preprocessed string
    /// </returns>
    public string Final();
}