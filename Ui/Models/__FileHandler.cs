using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Ui.Models;

public class __FileHandler
{
    private ProcessRules m_rules;

    public void ProcessFiles(IEnumerable<string> inputFilePaths, string outputDirectory, ProcessRules rules)
    {
        m_rules = rules;
        foreach (var inputFilePath in inputFilePaths) ProcessFile(inputFilePath, outputDirectory);
    }

    public void ProcessFile(string inputFilePath, string outputDirectory)
    {
        try
        {
            using var reader = new StreamReader(inputFilePath);
            using var writer = new StreamWriter(FormOutputPath(inputFilePath, outputDirectory));

            while (reader.ReadLine() is { } line)
            {
                if (m_rules.EraseDelimiters) line = RemoveDelimiters(line);
                line = RemoveLongWords(line);
                writer.WriteLineAsync(line);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Process file {Path.GetFileName(inputFilePath)} error: {e.Message}");
        }
    }

    private string RemoveLongWords(string input)
    {
        var result = new StringBuilder();
        var word = new StringBuilder();

        foreach (var c in input)
            if (char.IsLetterOrDigit(c))
            {
                word.Append(c);
            }
            else
            {
                if (word.Length <= m_rules.MaxWordLength) result.Append(word);

                result.Append(c);
                word.Clear();
            }

        if (word.Length <= m_rules.MaxWordLength) result.Append(word);

        return result.ToString();
    }

    private string RemoveDelimiters(string input)
    {
        var result = new StringBuilder();

        foreach (var c in input)
            if (!char.IsPunctuation(c))
                result.Append(c);

        return result.ToString();
    }

    private string FormOutputPath(string filePath, string outputDirectory)
    {
        return $"{outputDirectory}/{Path.GetFileNameWithoutExtension(filePath)}.processed{Path.GetExtension(filePath)}";
    }

    public struct ProcessRules
    {
        public int MaxWordLength;
        public bool EraseDelimiters;
    }
}