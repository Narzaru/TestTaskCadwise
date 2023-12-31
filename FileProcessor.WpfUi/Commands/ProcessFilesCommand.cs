﻿using System.Linq;
using System.Threading.Tasks;
using Ui.Models;
using Ui.ViewModels;

namespace Ui.Commands;

public class ProcessFilesCommand : AsyncCommandBase
{
    private MainWindowViewModel m_mainWindowViewModel;

    public ProcessFilesCommand(MainWindowViewModel mainWindowViewModel)
    {
        m_mainWindowViewModel = mainWindowViewModel;
        m_mainWindowViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(m_mainWindowViewModel.OutputDirectory) ||
                args.PropertyName == nameof(m_mainWindowViewModel.ProcessInProgress))
                OnCanExecutedChanged();
        };

        m_mainWindowViewModel.Files.CollectionChanged += (sender, args) => { OnCanExecutedChanged(); };
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        m_mainWindowViewModel.ProcessInProgress = true;
        var fileHandler = new FileProcessorFacade();

        var removeDelimiters = m_mainWindowViewModel.RemoveDelimiters;
        if (!int.TryParse(m_mainWindowViewModel.MaxWordLength, out var maxWordLength)) maxWordLength = -1;

        string delimiters = removeDelimiters ? ",.;:!?-'`()[]{}<>/\\|\t" : "";
        await fileHandler.ProcessFiles(
            m_mainWindowViewModel.Files,
            m_mainWindowViewModel.OutputDirectory,
            maxWordLength,
            delimiters);
        m_mainWindowViewModel.ProcessInProgress = false;
    }

    public override bool CanExecute(object? parameter)
    {
        return m_mainWindowViewModel.OutputDirectory != "Not selected" && m_mainWindowViewModel.Files.Any() &&
               !m_mainWindowViewModel.ProcessInProgress;
    }
}