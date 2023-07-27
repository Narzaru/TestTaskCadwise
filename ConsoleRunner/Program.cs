using Implementation;
using Implementation.TextProcessors;
using Interfaces;

var fileReader = new PiecewiseFileReader("TestFile.txt");
var delimitersCleaner = new DelimitersCleaner("!, ");
var wordRemover = new WordRemover(6);

var fileProcessor =
    new FileProcessor(
        new IPiecewiseReader[] { fileReader },
        Array.Empty<IPiecewiseTextWrite>(),
        new IPiecewiseTextProcessor[] { delimitersCleaner, wordRemover });

await fileProcessor.Run();