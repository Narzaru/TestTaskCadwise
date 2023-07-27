using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Implementation;
using Implementation.TextProcessors;
using Interfaces;

namespace Ui.Models;

public class FileProcessorFacade
{
    public async Task ProcessFiles(
        IEnumerable<string> files,
        string outputDirectory,
        int minWordLength,
        IEnumerable<char> delimiters)
    {
        _notProcessedFiles.Clear();

        var wordRemover = new WordRemover(minWordLength);
        var delimiterRemover = new DelimitersCleaner(delimiters);

        var fileReaders = files.Select(file =>
        {
            try
            {
                return new PiecewiseFileFileReader(file, 1024);
            }
            catch
            {
                _notProcessedFiles.Add(file);
                return null;
            }
        }).Where(fr => fr is not null).ToList();

        if (!outputDirectory.EndsWith('\\')) outputDirectory += '\\';
        var fileWriterFabric = new PiecewiseFileWriterFabric(outputDirectory, "_processed");

        var filerProcessor = new FileProcessor(
            fileReaders,
            new IPiecewiseTextProcessor[] { delimiterRemover, wordRemover },
            fileWriterFabric);

        await filerProcessor.Run();
    }

    public IEnumerable<string> NotProcessedFiles => _notProcessedFiles;

    private List<string> _notProcessedFiles = new();
}