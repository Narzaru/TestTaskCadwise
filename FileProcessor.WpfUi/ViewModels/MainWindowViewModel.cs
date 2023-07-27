using System.Collections.ObjectModel;
using Ui.Commands;

namespace Ui.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private string m_outputDirectory;

    private bool m_processInProgress;

    public MainWindowViewModel()
    {
        m_outputDirectory = string.Empty;
        MaxWordLength = "0";
        Files = new ObservableCollection<string>();
        m_processInProgress = false;
        OpenFilesDialogCommand = new OpenFilesDialogCommand(this);
        OpenOutputDirectoryCommand = new OpenOutputDirectoryCommand(this);
        ProcessFilesCommand = new ProcessFilesCommand(this);
    }

    public string OutputDirectory
    {
        get
        {
            if (string.IsNullOrEmpty(m_outputDirectory)) return "Not selected";

            return m_outputDirectory;
        }
        set
        {
            m_outputDirectory = value;
            OnPropertyChanged();
        }
    }

    public string MaxWordLength { get; set; }

    public bool RemoveDelimiters { get; set; }

    public OpenFilesDialogCommand OpenFilesDialogCommand { get; set; }

    public ObservableCollection<string> Files { get; set; }

    public OpenOutputDirectoryCommand OpenOutputDirectoryCommand { get; set; }

    public ProcessFilesCommand ProcessFilesCommand { get; set; }

    public bool ProcessInProgress
    {
        get => m_processInProgress;
        set
        {
            m_processInProgress = value;
            OnPropertyChanged();
        }
    }
}