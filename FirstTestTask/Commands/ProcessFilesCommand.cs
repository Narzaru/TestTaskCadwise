using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.ViewModels;

namespace TestTask.Commands;

public class ProcessFilesCommand : AsyncCommandBase
{
    public ProcessFilesCommand(MainWindowViewModel mainWindowViewModel)
    {
        m_mainWindowViewModel = mainWindowViewModel;
        m_mainWindowViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(m_mainWindowViewModel.OutputDirectory) ||
                args.PropertyName == nameof(m_mainWindowViewModel.ProcessInProgress))
            {
                OnCanExecutedChanged();
            }
        };

        m_mainWindowViewModel.Files.CollectionChanged += (sender, args) => { OnCanExecutedChanged(); };
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        m_mainWindowViewModel.ProcessInProgress = true;
        var fileHandler = new FileHandler();

        var removeDelimiters = m_mainWindowViewModel.RemoveDelimiters;
        if (!int.TryParse(m_mainWindowViewModel.MaxWordLength, out int maxWordLength))
        {
            maxWordLength = -1;
        }

        var rules = new FileHandler.ProcessRules()
        {
            EraseDelimiters = removeDelimiters,
            MaxWordLength = maxWordLength
        };

        await Task.Run(() =>
        {
            fileHandler.ProcessFiles(m_mainWindowViewModel.Files, m_mainWindowViewModel.OutputDirectory, rules);
        });
        m_mainWindowViewModel.ProcessInProgress = false;
    }

    public override bool CanExecute(object? parameter)
    {
        return m_mainWindowViewModel.OutputDirectory != "Not selected" && m_mainWindowViewModel.Files.Any() &&
               !m_mainWindowViewModel.ProcessInProgress;
    }

    private MainWindowViewModel m_mainWindowViewModel;
}