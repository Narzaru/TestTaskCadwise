using System.IO;
using Microsoft.Win32;
using Ui.ViewModels;

namespace Ui.Commands;

public class OpenFilesDialogCommand : CommandBase
{
    private MainWindowViewModel m_mainWindowViewModel;

    public OpenFilesDialogCommand(MainWindowViewModel mainWindowViewModel)
    {
        m_mainWindowViewModel = mainWindowViewModel;
        m_mainWindowViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(m_mainWindowViewModel.ProcessInProgress)) OnCanExecutedChanged();
        };
    }

    public override void Execute(object? parameter)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "All files|*",
            Multiselect = true
        };

        if (dialog.ShowDialog() == true)
        {
            m_mainWindowViewModel.Files.Clear();
            foreach (var dialogFileName in dialog.FileNames)
            {
                m_mainWindowViewModel.Files.Add(dialogFileName);
                m_mainWindowViewModel.OutputDirectory = Path.GetDirectoryName(dialogFileName) ?? string.Empty;
            }
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !m_mainWindowViewModel.ProcessInProgress;
    }
}