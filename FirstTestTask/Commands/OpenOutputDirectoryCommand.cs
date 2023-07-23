using System.Windows.Forms;
using TestTask.ViewModels;

namespace TestTask.Commands;

public class OpenOutputDirectoryCommand : CommandBase
{
    public OpenOutputDirectoryCommand(MainWindowViewModel mainWindowViewModel)
    {
        m_mainWindowViewModel = mainWindowViewModel;
        m_mainWindowViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(mainWindowViewModel.ProcessInProgress))
            {
                OnCanExecutedChanged();
            }
        };
    }

    public override void Execute(object? parameter)
    {
        var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            m_mainWindowViewModel.OutputDirectory = dialog.SelectedPath;
        }
    }

    public override bool CanExecute(object? parameter)
        => !m_mainWindowViewModel.ProcessInProgress;

    private readonly MainWindowViewModel m_mainWindowViewModel;
}