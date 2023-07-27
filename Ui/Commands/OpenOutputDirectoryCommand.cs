using System.Windows.Forms;
using Ui.ViewModels;

namespace Ui.Commands;

public class OpenOutputDirectoryCommand : CommandBase
{
    private readonly MainWindowViewModel m_mainWindowViewModel;

    public OpenOutputDirectoryCommand(MainWindowViewModel mainWindowViewModel)
    {
        m_mainWindowViewModel = mainWindowViewModel;
        m_mainWindowViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(mainWindowViewModel.ProcessInProgress)) OnCanExecutedChanged();
        };
    }

    public override void Execute(object? parameter)
    {
        var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();
        if (result == DialogResult.OK) m_mainWindowViewModel.OutputDirectory = dialog.SelectedPath;
    }

    public override bool CanExecute(object? parameter)
    {
        return !m_mainWindowViewModel.ProcessInProgress;
    }
}